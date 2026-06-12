using MediatR;
using ShopDTO.DTOs;
using ShopInfrastructure.Data;
using Microsoft.EntityFrameworkCore;
using ShopApplication.Queries;

namespace ShopApplication.Handlers
{
    public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, GetOrderResponse>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetOrderQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetOrderResponse> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            // Validate input
            if (request.OrderId <= 0)
            {
                throw new ArgumentException("Order ID must be greater than 0.");
            }

            // Fetch order with details
            var order = await _dbContext.Orders
                .Include(o => o.OrderDetails)
                .FirstOrDefaultAsync(o => o.Id == request.OrderId, cancellationToken);

            if (order == null)
            {
                throw new InvalidOperationException($"Order with ID {request.OrderId} not found.");
            }

            // Return response DTO
            return new GetOrderResponse
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
                OrderDetails = order.OrderDetails.Select(od => new OrderDetailResponse
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
    }
}
