namespace DomainLayer.Models
{
    public class Specialization
    {
        
            public int SpecializationId { get; set; }
            public string NameAr { get; set; }
            public string NameEn { get; set; }

            
            public ICollection<Doctor>? Doctors { get; set; }

       
    }

    
}
