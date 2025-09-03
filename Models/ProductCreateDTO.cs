using System.ComponentModel.DataAnnotations;

namespace E_CommerceSystem.Models
{
    public class ProductCreateDTO
    {
        [Required]
        public string ProductName { get; set; }

        public string Description { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Stock { get; set; }

        public int CategoryId { get; set; }

        public IFormFile? ImageFile { get; set; } // For image upload
    }
}
