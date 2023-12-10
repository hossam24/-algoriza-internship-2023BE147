using DomainLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.DTO
{
   public class DoctorWithSpecializtionDto
    {
        
        public string? Image { get; set; }
        public string ?FullName { get; set; }
        public string ?Email { get; set; }
        public string ?Phone { get; set; }
        public Gender ?Gender { get; set; }
        public DateTime ?DateOfBirth { get; set; }
       public string  Password { get; set; } 
        public int ?SpecializationId { get; set;}
        public string ?SpecializationNameEN { get; set; }
        public string ?SpecializationNameAR { get; set; }


    }
}
