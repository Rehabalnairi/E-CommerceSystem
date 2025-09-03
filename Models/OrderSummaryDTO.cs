namespace E_CommerceSystem.Models
{
    public class OrderSummaryDTO
    {
        public int OID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string UName { get; set; }
        public List<OrderItemDTO> Items { get; set; }
    }

    public class OrderProductDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }

}
