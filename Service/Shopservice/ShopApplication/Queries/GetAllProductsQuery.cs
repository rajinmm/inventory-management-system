using MediatR;
using ShopDTO.DTOs;

namespace ShopApplication.Queries
{
    public record GetAllProductsQuery : IRequest<List<GetAllProductsResponse>>;
}
