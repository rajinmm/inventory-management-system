using MediatR;
using ShopDomain.Entities;
using ShopDTO.DTOs;
using ShopInfrastructure.Data;
using ShopApplication.Commands;

namespace ShopApplication.Handlers
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CreateCategoryResponse>
    {
        private readonly ApplicationDbContext _dbContext;

        public CreateCategoryCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CreateCategoryResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(request.CategoryName))
            {
                throw new ArgumentException("Category name cannot be empty.");
            }

            // Check if category already exists
            var categoryExists = _dbContext.Categories.Any(c => c.CategoryName == request.CategoryName);
            if (categoryExists)
            {
                throw new InvalidOperationException($"Category '{request.CategoryName}' already exists.");
            }

            // Create category entity
            var category = new Category
            {
                CategoryName = request.CategoryName,
                CreatedAt = DateTime.UtcNow
            };

            // Add to database
            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync(cancellationToken);

            // Return response DTO
            return new CreateCategoryResponse
            {
                Id = category.Id,
                CategoryName = category.CategoryName,
                CreatedAt = category.CreatedAt,
                UpdatedAt = category.UpdatedAt
            };
        }
    }
}
