using DomainLayer.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.DTO
{
     public class RequestDto
    {
        public string? DoctorImage { get; set; }
        public string ?DoctorName { get; set; }
        public string? SpecializationName { get; set; }
        public Days? day { get; set; }
        public IEnumerable<TimeSpan>? time { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? FinalPrice { get; set; }
        public RequestStatus? Status { get; set; }
        public string? DiscoundCode { get; set; }


    }
}
