using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.DTO
{
    public class PatientWithRequestDto
    {
        public PatientDto Detailes { get; set; }
        public List<RequestDto> Requests { get; set; }



    }
}
