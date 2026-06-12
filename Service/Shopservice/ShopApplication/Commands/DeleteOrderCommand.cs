using MediatR;

namespace ShopApplication.Commands
{
    public record DeleteOrderCommand(int OrderId) : IRequest<DeleteOrderResponse>;

    public class DeleteOrderResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int OrderId { get; set; }
    }
}
