using System.ComponentModel.DataAnnotations;

namespace LBm295.Models
{
    public class PublisherDto
    {
        
        public Guid Id { get; set; }
        public string PublisherName { get; set; }
        public int CompanyWorth { get; set; }
        public int FoundingYear { get; set; }
    }
}
