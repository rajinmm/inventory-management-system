using ShopInfrastructure.Data;
using ShopApplication.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ShopApplication.Handlers
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, DeleteOrderResponse>
    {
        private readonly ApplicationDbContext _dbContext;

        public DeleteOrderCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DeleteOrderResponse> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
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

            // Check if order exists
            if (order == null)
            {
                throw new InvalidOperationException($"Order with ID {request.OrderId} not found.");
            }

            // Delete order details first (due to foreign key constraint)
            _dbContext.OrderDetails.RemoveRange(order.OrderDetails);

            // Delete order
            _dbContext.Orders.Remove(order);

            // Save changes
            await _dbContext.SaveChangesAsync(cancellationToken);

            // Return success response
            return new DeleteOrderResponse
            {
                Success = true,
                Message = $"Order with ID {request.OrderId} has been successfully deleted.",
                OrderId = request.OrderId
            };
        }
    }
}
