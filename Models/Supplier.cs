using System.ComponentModel.DataAnnotations;

namespace E_CommerceSystem.Models
{
    public class Supplier
    {
        [Key]
        public int SupplierId { get; set; }
        
        [Required]
        public string SupplierName { get; set; }

        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$",
        ErrorMessage = "Invalid email format.(e.g 'example@gmail.com')")]
        public string ContactEmail { get; set; }

        [Required]
        public string Phone { get; set; }


    }
}
