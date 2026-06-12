using MediatR;
using ShopDTO.DTOs;

namespace ShopApplication.Commands
{
    public record CreateCategoryCommand(string CategoryName) : IRequest<CreateCategoryResponse>;
}
