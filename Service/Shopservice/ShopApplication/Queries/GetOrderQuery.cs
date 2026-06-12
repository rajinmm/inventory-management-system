using MediatR;
using ShopDTO.DTOs;

namespace ShopApplication.Queries
{
    public record GetOrderQuery(int OrderId) : IRequest<GetOrderResponse>;
}
