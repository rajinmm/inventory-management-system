using ShopDomain.Entities;
using ShopDTO.DTOs;
using ShopInfrastructure.Data;
using ShopApplication.Commands;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace ShopApplication.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CreateOrderResponse>
    {
        private readonly ApplicationDbContext _dbContext;
        private const decimal MaxTotalDiscount = 500m;

        public CreateOrderCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CreateOrderResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(request.CustName))
            {
                throw new ArgumentException("Customer name cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(request.CustPhone))
            {
                throw new ArgumentException("Customer phone cannot be empty.");
            }

            if (request.OrderDetails == null || request.OrderDetails.Count == 0)
            {
                throw new ArgumentException("Order must contain at least one product.");
            }

            // Validate that all products exist and fetch with categories
            var productIds = request.OrderDetails.Select(od => od.ProductId).Distinct().ToList();
            var products = await _dbContext.Products
                .Include(p => p.Category)
                .Where(p => productIds.Contains(p.Id))
                .ToListAsync(cancellationToken);

            if (products.Count != productIds.Count)
            {
                throw new InvalidOperationException("One or more products do not exist.");
            }

            // Create order entity
            var order = new Order
            {
                CustName = request.CustName,
                CustPhone = request.CustPhone,
                OrderNote = request.OrderNote,
                OrderStatus = request.OrderStatus,
                GrossAmount = 0,
                NetAmount = 0,
                CreatedAt = DateTime.UtcNow
            };

            decimal totalGrossAmount = 0;
            decimal totalDiscount = 0;

            // Create order details with discount calculation
            var orderDetails = new List<OrderDetail>();
            var discountDetails = new Dictionary<int, decimal>(); // ProductId -> discount percentage

            // First pass: Calculate discounts based on category-specific logic
            CalculateDiscounts(request.OrderDetails, products, discountDetails);

            // Second pass: Create order details and apply discounts
            foreach (var detailRequest in request.OrderDetails)
            {
                var product = products.FirstOrDefault(p => p.Id == detailRequest.ProductId);
                if (product == null)
                {
                    throw new InvalidOperationException($"Product with ID {detailRequest.ProductId} not found.");
                }

                var grossAmount = product.Amount * detailRequest.Qty;
                var discountPercentage = discountDetails[detailRequest.ProductId];
                var discountAmount = grossAmount * (discountPercentage / 100);
                var netAmount = grossAmount - discountAmount;

                var orderDetail = new OrderDetail
                {
                    ProductId = detailRequest.ProductId,
                    Qty = detailRequest.Qty,
                    GrossAmount = grossAmount,
                    NetAmount = netAmount
                };

                orderDetails.Add(orderDetail);
                totalGrossAmount += grossAmount;
                totalDiscount += discountAmount;
            }

            // Apply maximum discount cap
            decimal totalNetAmount = totalGrossAmount - totalDiscount;
            if (totalDiscount > MaxTotalDiscount)
            {
                totalDiscount = MaxTotalDiscount;
                totalNetAmount = totalGrossAmount - MaxTotalDiscount;

                // Redistribute discount equally to products
                var discountPerProduct = MaxTotalDiscount / orderDetails.Count;
                foreach (var detail in orderDetails)
                {
                    detail.NetAmount = detail.GrossAmount - discountPerProduct;
                }
            }

            // Set totals
            order.GrossAmount = totalGrossAmount;
            order.NetAmount = totalNetAmount;

            // Add order and details to database
            _dbContext.Orders.Add(order);
            foreach (var detail in orderDetails)
            {
                detail.OrderId = order.Id;
                order.OrderDetails.Add(detail);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            // Return response DTO
            return new CreateOrderResponse
            {
                Id = order.Id,
                CustName = order.CustName,
                CustPhone = order.CustPhone,
                OrderNote = order.OrderNote,
                OrderStatus = order.OrderStatus,
                GrossAmount = order.GrossAmount,
                NetAmount = order.NetAmount,
                CreatedAt = order.CreatedAt,
                UpdatedAt = order.UpdatedAt,
                OrderDetails = orderDetails.Select(od => new OrderDetailResponse
                {
                    Id = od.Id,
                    OrderId = od.OrderId,
                    ProductId = od.ProductId,
                    Qty = od.Qty,
                    GrossAmount = od.GrossAmount,
                    NetAmount = od.NetAmount
                }).ToList()
            };
        }

        /// <summary>
        /// Calculates discounts based on category-specific rules
        /// </summary>
        private void CalculateDiscounts(
            List<CreateOrderDetailRequest> orderDetails,
            List<Product> products,
            Dictionary<int, decimal> discountDetails)
        {
            // Group products by category
            var productsByCategory = products.GroupBy(p => p.Category.CategoryName).ToList();

            foreach (var categoryGroup in productsByCategory)
            {
                var categoryName = categoryGroup.Key;
                var categoryProducts = categoryGroup.ToList();

                if (categoryName == "HomeGoods")
                {
                    ApplyHomeGoodsDiscount(orderDetails, categoryProducts, discountDetails);
                }
                else if (categoryName == "Clothing")
                {
                    ApplyClothingDiscount(orderDetails, categoryProducts, discountDetails);
                }
                else
                {
                    // Apply base discount for other categories
                    ApplyBaseDiscount(categoryProducts, discountDetails);
                }
            }
        }

        /// <summary>
        /// HomeGoods Discount Logic:
        /// - Qty > 2: 10% discount
        /// - Qty = 2: 5% discount
        /// - Qty = 1: base discount
        /// </summary>
        private void ApplyHomeGoodsDiscount(
            List<CreateOrderDetailRequest> orderDetails,
            List<Product> products,
            Dictionary<int, decimal> discountDetails)
        {
            foreach (var product in products)
            {
                var orderDetail = orderDetails.FirstOrDefault(od => od.ProductId == product.Id);
                if (orderDetail == null)
                    continue;

                decimal discount;
                if (orderDetail.Qty > 2)
                {
                    discount = 10m;
                }
                else if (orderDetail.Qty == 2)
                {
                    discount = 5m;
                }
                else
                {
                    discount = (decimal)product.BaseDiscountInPercentage;
                }

                discountDetails[product.Id] = discount;
            }
        }

        /// <summary>
        /// Clothing Discount Logic:
        /// - Total 3 or more items: 15% discount on all items
        /// - Less than 3 items: base discount
        /// </summary>
        private void ApplyClothingDiscount(
            List<CreateOrderDetailRequest> orderDetails,
            List<Product> products,
            Dictionary<int, decimal> discountDetails)
        {
            // Count total quantity of clothing items
            var totalClothingQty = 0;
            var clothingProductIds = products.Select(p => p.Id).ToList();

            foreach (var orderDetail in orderDetails)
            {
                if (clothingProductIds.Contains(orderDetail.ProductId))
                {
                    totalClothingQty += orderDetail.Qty;
                }
            }

            // Apply discount based on total quantity
            decimal discount = totalClothingQty >= 3 ? 15m : 0m;

            foreach (var product in products)
            {
                var orderDetail = orderDetails.FirstOrDefault(od => od.ProductId == product.Id);
                if (orderDetail == null)
                    continue;

                if (totalClothingQty >= 3)
                {
                    discountDetails[product.Id] = 15m;
                }
                else
                {
                    discountDetails[product.Id] = (decimal)product.BaseDiscountInPercentage;
                }
            }
        }

        /// <summary>
        /// Apply base discount for other categories
        /// </summary>
        private void ApplyBaseDiscount(
            List<Product> products,
            Dictionary<int, decimal> discountDetails)
        {
            foreach (var product in products)
            {
                discountDetails[product.Id] = (decimal)product.BaseDiscountInPercentage;
            }
        }
    }
}
