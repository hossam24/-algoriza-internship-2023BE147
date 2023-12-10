using DomainLayer.DTO;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Repository
{
    public interface IDoctorRepo
    {
        List<DoctorWithSpecializtionDto> GetAll(int pageNumber, int pageSize, string search);
        DoctorWithSpecializtionDto GetById(string id);
        
        void UPDATE(string id,DoctorWithSpecializtionDto item);

        void DELETE(string id);

        int NumberOfDoctor();
        List<TopDoctorsDTO> Top10Doctor();
        List<Top5SpecializeDTO> Top5Specialize();

        List<DoctorDto> SearchonDoctors(int pageNumber, int pageSize, string search);

       



    }
}
