using MediatR;
using ShopDomain.Entities;
using ShopDTO;
using ShopInfrastructure.Data;
using ShopApplication.Commands;
using ShopDTO.DTOs;

namespace ShopApplication.Handlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductResponse>
    {
        private readonly ApplicationDbContext _dbContext;

        public CreateProductCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CreateProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            // Validate category exists
            var categoryExists = _dbContext.Categories.Any(c => c.Id == request.CategoryId);
            if (!categoryExists)
            {
                throw new ArgumentException($"Category with ID {request.CategoryId} does not exist.");
            }

            // Create product entity
            var product = new Product
            {
                Name = request.Name,
                Amount = request.Amount,
                Description = request.Description,
                CategoryId = request.CategoryId,
                BaseDiscountInPercentage = request.BaseDiscountInPercentage,
                CreatedAt = DateTime.UtcNow
            };

            // Add to database
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync(cancellationToken);

            // Return response DTO
            return new CreateProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Amount = product.Amount,
                Description = product.Description,
                CategoryId = product.CategoryId,
                BaseDiscountInPercentage = product.BaseDiscountInPercentage,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt
            };
        }
    }
}
