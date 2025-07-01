using System.ComponentModel.DataAnnotations;

namespace AmazeCare.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Message { get; set; }
    }
}
