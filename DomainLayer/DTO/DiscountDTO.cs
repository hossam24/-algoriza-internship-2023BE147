using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.DTO
{
    public class DiscountDTO
    {
        public string DiscountCode { get; set; }
        public DiscountType Discounttype { get; set;}
        public decimal Value { get; set; }  

        public int ComletedREquested {
            get; set; }

        
        


    }
}
