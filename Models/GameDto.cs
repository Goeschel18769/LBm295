using System.ComponentModel.DataAnnotations;

namespace LBm295.Models
{
    public class GameDto
    {
        
        public Guid Id { get; set; }
        public string GameName { get; set; }
        public int Revenue { get; set; }
        public string ReleaseDate { get; set; }
    }
}
