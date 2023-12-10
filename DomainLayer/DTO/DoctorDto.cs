using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.DTO
{
    public class DoctorDto
    {
        public string? Image { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public Gender? Gender { get; set; }
        public string? SpecializationNameEN { get; set; }
        public string? SpecializationNameAR { get; set; }
        public decimal? price { get; set; } 
        public IEnumerable< Appointment >?appointment { get; set; } 

        ////public IEnumerable<TimeSpan> time { get; set; }    
        ////public Days days { get; set; } 
           

    }
}
