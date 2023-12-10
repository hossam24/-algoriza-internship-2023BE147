using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Times
    {
        public int TimesId { get; set; }
        public TimeSpan Time { get; set; }
        public int AppointmentId { get; set; }
        public virtual Appointment ?Appointments { get; set; }
        
       



    }
}
