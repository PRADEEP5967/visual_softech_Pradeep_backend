using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Username { get; set; }
        [Required]
        public string PasswordHash { get; set; }
    }
}
