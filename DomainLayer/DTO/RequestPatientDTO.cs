using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.DTO
{
    public  class RequestPatientDTO
    {

        public string PatientName { get; set; }
        public string Image { get; set;}

        public int age { get; set;}
        public Gender gender { get; set;}
        public string phone { get; set;}
        public string Email { get; set;}

        public Days day { get; set; }
        public IEnumerable<TimeSpan> time { get; set; }





    }
}
