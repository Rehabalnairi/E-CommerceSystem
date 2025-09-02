namespace E_CommerceSystem.Models
{
    public class SupplierDTO
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string? ContactEmail { get; set; }
        public string Phone { get; set; }

        public int ProductCount { get; set; }
    }
}
