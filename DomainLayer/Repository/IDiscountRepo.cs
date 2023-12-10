using DomainLayer.DTO;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Repository
{
    public interface IDiscountRepo
    {
        void Add(DiscountDTO item); 
        void Update(int id,DiscountDTO item); 
        void Delete(int id);
        void DeActivated(int id);
    }
}
