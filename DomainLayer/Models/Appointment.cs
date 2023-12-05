namespace DomainLayer.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int DoctorId { get; set; }
        public Doctor? Doctor { get; set; }
        public Days Day { get; set; }
       public List<Times> times { get; set; }

    }
}
