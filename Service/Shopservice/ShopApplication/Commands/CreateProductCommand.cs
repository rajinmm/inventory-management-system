using MediatR;
using ShopDTO;
using ShopDTO.DTOs;

namespace ShopApplication.Commands
{

     public record CreateProductCommand(string Name, decimal Amount, string Description, int CategoryId, float BaseDiscountInPercentage) : IRequest<CreateProductResponse>;
}
