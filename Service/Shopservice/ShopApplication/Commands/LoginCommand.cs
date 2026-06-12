using MediatR;
using ShopDTO.DTOs;

namespace ShopApplication.Commands
{
    public record LoginCommand(string UserLogin, string Password) : IRequest<LoginResponse>;
}
