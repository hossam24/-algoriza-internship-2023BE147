using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.DTO
{
   public class TopDoctorsDTO
    {
      public string? Image { get; set; }
        public string FullName { get; set; }
        public string Specialize { get; set; }
        public int Requests { get; set; }    

    }
}
