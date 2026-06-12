namespace ShopDTO.DTOs
{
    public class LoginRequest
    {
        public string UserLogin { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponse
    {
        public int UserId { get; set; }
        public string UserLogin { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        public string TokenType { get; set; } = "Bearer";
        public int ExpiresIn { get; set; }
        public DateTime IssuedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
    }

    public class AuthTokenRequest
    {
        public string UserLogin { get; set; }
        public string Password { get; set; }
    }
}
