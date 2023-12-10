using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Models
{
    public class DiscountCode
    {
        
            public int DiscountCodeId { get; set; }
            public string? Code { get; set; }
            [ForeignKey("Request")]  
            public int CompletedRequests { get; set; }
            public virtual Request? Request { get; set; }
            public DiscountType DiscountType { get; set; }
            [Column(TypeName = "decimal(18, 4)")]
             public decimal Value { get; set; }
        public bool IsActive { get; set; }=true;
    }

       

    
}
