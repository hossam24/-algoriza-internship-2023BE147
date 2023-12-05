using DomainLayer.DTO;
using DomainLayer.Models;
using DomainLayer.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFLayer.Repository
{
    public class DoctorRepo: IDoctorRepo
    {
        private readonly MyContext myContext;
        private readonly UserManager<ApplicationUser> userManager;

        public DoctorRepo(MyContext myContext,UserManager<ApplicationUser> userManager)
        {
            this.myContext = myContext;
            this.userManager = userManager;
        }


        public List<DoctorWithSpecializtionDto> GetAll()
        {
            var DoctorWithspecialization = myContext.Doctors
             .Include(d => d.Specialization)
             .Select(d => new DoctorWithSpecializtionDto
             {
                 Image = d.ApplicationUser.Image,
                 FullName = d.ApplicationUser.FullName,
                 Email = d.ApplicationUser.Email,
                 Phone = d.ApplicationUser.Phone,
                 Gender = (Gender)d.ApplicationUser.Gender,
                 SpecializationNameEN = d.Specialization.NameAr,
                 SpecializationNameAR = d.Specialization.NameEn
             })
             .ToList();

            return DoctorWithspecialization;
        }

        public DoctorWithSpecializtionDto GetById(string id)
        {
            var DoctorWithspecialization = myContext.Doctors
             .Include(d => d.Specialization)
             .Where(d=>d.Id == id)
             .Select(d => new DoctorWithSpecializtionDto
             {
                 Image = d.ApplicationUser.Image,
                 FullName = d.ApplicationUser.FullName,
                 Email = d.ApplicationUser.Email,
                 Phone = d.ApplicationUser.Phone,
                 Gender = (Gender)d.ApplicationUser.Gender,
                 SpecializationNameEN = d.Specialization.NameAr,
                 SpecializationNameAR = d.Specialization.NameEn
             }).FirstOrDefault();
            return (DoctorWithspecialization);
        }

        public void UPDATE(DoctorWithSpecializtionDto item, int id)
        {
            throw new NotImplementedException();
        }

            

    }
   
}
