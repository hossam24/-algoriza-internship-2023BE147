using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Models
{
    public class Patient
    {
  
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; } 
        public virtual ApplicationUser ApplicationUser { get; set; }
        public List<Request>? Requests { get; set; }
    }
}
