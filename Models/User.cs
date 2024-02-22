using System.ComponentModel.DataAnnotations;

namespace LBm295.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }    
        public string Username { get; set; }
        public string PasswordHash { get; set; }    
    }
}
