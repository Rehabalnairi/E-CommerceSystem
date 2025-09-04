using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_CommerceSystem.Models
{
    public class RefreshToken
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        public DateTime Expires { get; set; }
        public bool IsRevoked { get; set; } = false;

        [Required]
        public int UID { get; set; }

        public User User { get; set; }
        public DateTime Created { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
    }
}
