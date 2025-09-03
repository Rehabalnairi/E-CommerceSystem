using System.ComponentModel.DataAnnotations;

namespace E_CommerceSystem.Models
{
    public class RefreshToken
    {
        [Key]
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public bool IsRevoked { get; set; } = false;

        public int UID { get; set; }
        public User User { get; set; }
        public DateTime Created { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
    }
}
