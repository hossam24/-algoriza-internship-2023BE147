using DomainLayer.DTO;
using DomainLayer.Models;
using DomainLayer.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFLayer.Repository
{
    public class DiscountCodeRepo:IDiscountRepo
    {
        private readonly MyContext myContext;

        public DiscountCodeRepo(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public void Add(DiscountDTO item)
        {

           
            var discountCode = new DiscountCode
            {
                Code = item.DiscountCode,
                DiscountType = item.Discounttype,
                Value = item.Value,
               // CompletedRequests = item.ComletedREquested
                
            };
          

            myContext.DiscountCodes.Add(discountCode);
            myContext.SaveChanges();
        }

        public void DeActivated(int id)
        {
            var code = myContext.DiscountCodes.FirstOrDefault(d => d.DiscountCodeId == id);
            if (code != null) {
                code.IsActive = false;
               myContext.SaveChanges(); 
            }
        }

        public void Delete(int id)
        {
            DiscountCode code= myContext.DiscountCodes.FirstOrDefault(d => d.DiscountCodeId == id);
            myContext.Remove(code);
            myContext.SaveChanges();
        
        }

        public void Update(int id, DiscountDTO item)
        {
            DiscountCode discountCode = myContext.DiscountCodes
               .FirstOrDefault(dc => dc.DiscountCodeId==id);

            if (discountCode != null && discountCode.Request == null)
            {

                discountCode.Code = item.DiscountCode;
                discountCode.DiscountType = item.Discounttype;
                discountCode.Value = item.Value;
                discountCode.CompletedRequests = item.ComletedREquested;

                myContext.SaveChanges();
            }
          
        }
    }
}
