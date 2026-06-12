using MediatR;
using Microsoft.Extensions.Configuration;
using ShopApplication.Commands;
using ShopApplication.Services;
using ShopDTO.DTOs;
using ShopInfrastructure.Data;

namespace ShopApplication.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;

        public LoginCommandHandler(ApplicationDbContext dbContext, ITokenService tokenService, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _tokenService = tokenService;
            _configuration = configuration;
        }

        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(request.UserLogin) || string.IsNullOrWhiteSpace(request.Password))
            {
                throw new ArgumentException("UserLogin and Password are required.");
            }

            // Find user by username
            var user = _dbContext.Users.FirstOrDefault(u => u.UserLogin == request.UserLogin);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid username or password.");
            }

            // Verify password (in production, use proper password hashing like BCrypt)
            if (user.Password != request.Password)
            {
                throw new UnauthorizedAccessException("Invalid username or password.");
            }

            // Get expiration time from configuration
            var expirationMinutes = int.Parse(_configuration["JwtSettings:ExpirationMinutes"] ?? "60");

            // Generate token
            var loginResponse = _tokenService.GenerateToken(user, expirationMinutes);

            return await Task.FromResult(loginResponse);
        }
    }
}
