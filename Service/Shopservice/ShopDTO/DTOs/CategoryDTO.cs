namespace ShopDTO.DTOs
{
    public class CreateCategoryRequest
    {
        public string CategoryName { get; set; }
    }

    public class CreateCategoryResponse
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class GetAllCategoriesResponse
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
