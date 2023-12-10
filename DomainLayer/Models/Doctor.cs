using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Models
{
    public class Doctor
    {
            
            public int SpecializationId { get; set; }
            public Specialization Specialization { get; set; }
            [Column(TypeName = "decimal(18, 4)")]
              public decimal ?Price { get; set; }

             [ForeignKey("ApplicationUser")]
             public string Id { get; set; } 
              public virtual ApplicationUser ApplicationUser { get; set; }
              public List<Request>? Requests { get; set; }
              public List<Appointment>? Appointments { get; set; }


    }




}
