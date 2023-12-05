using DomainLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Repository
{
    public interface IDoctorRepo
    {
        List<DoctorWithSpecializtionDto> GetAll();
        DoctorWithSpecializtionDto GetById(string id);
        
        void UPDATE(DoctorWithSpecializtionDto item, int id);

       



    }
}
