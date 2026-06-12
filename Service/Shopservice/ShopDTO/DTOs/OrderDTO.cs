namespace ShopDTO.DTOs
{
    public class CreateOrderRequest
    {
        public string CustName { get; set; }
        public string CustPhone { get; set; }
        public string OrderNote { get; set; }
        public int OrderStatus { get; set; }
        public List<CreateOrderDetailRequest> OrderDetails { get; set; } = new List<CreateOrderDetailRequest>();
    }

    public class CreateOrderDetailRequest
    {
        public int ProductId { get; set; }
        public int Qty { get; set; }
    }

    public class CreateOrderResponse
    {
        public int Id { get; set; }
        public string CustName { get; set; }
        public string CustPhone { get; set; }
        public string OrderNote { get; set; }
        public int OrderStatus { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal NetAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<OrderDetailResponse> OrderDetails { get; set; } = new List<OrderDetailResponse>();
    }

    public class OrderDetailResponse
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Qty { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal NetAmount { get; set; }
    }

    public class GetOrderResponse
    {
        public int Id { get; set; }
        public string CustName { get; set; }
        public string CustPhone { get; set; }
        public string OrderNote { get; set; }
        public int OrderStatus { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal NetAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<OrderDetailResponse> OrderDetails { get; set; } = new List<OrderDetailResponse>();
    }
}
