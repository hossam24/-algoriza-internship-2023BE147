using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        [ForeignKey("Doctor")]
        public string DoctorId { get; set; }
        public Doctor? Doctor { get; set; }
        public Days Day { get; set; }
       public List<Times>? times { get; set; }
        public  Request ?Request { get; set; }    

    }
}
