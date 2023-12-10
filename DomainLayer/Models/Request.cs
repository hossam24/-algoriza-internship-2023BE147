namespace DomainLayer.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    public class Request
    {
            public int RequestId { get; set; }
            [ForeignKey("doctor")]
            public string DoctorId { get; set; }
            public Doctor? doctor { get; set; }

             [ForeignKey("ApplicationUser")]
              public string UserId { get; set; }
              public ApplicationUser User { get; set; }

      

        [Column(TypeName = "decimal(18, 4)")]
            public decimal FinalPrice { get; set; }
            public int? DiscountCodeId { get; set; }
            public DiscountCode? DiscountCode { get; set; }
                  [ForeignKey("Appointment")]

                 public int AppointmentId { get; set; }
             public  Appointment? Appointment { get; set; }
            public RequestStatus Status { get; set; } = RequestStatus.Pending;
            
    }

    

    
}
