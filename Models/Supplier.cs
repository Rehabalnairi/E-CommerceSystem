using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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

        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; }
    }
}
