using MediatR;
using ShopDTO.DTOs;

namespace ShopApplication.Queries
{
    public record GetAllCategoriesQuery : IRequest<List<GetAllCategoriesResponse>>;
}
