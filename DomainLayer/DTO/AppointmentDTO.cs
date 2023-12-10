using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.DTO
{
    public class AppointmentDTO
    {
        public string DoctorId { get; set; }
       public decimal price { get; set; }
        public Days days { get; set; }
        public Times times { get; set; }



    }
}
