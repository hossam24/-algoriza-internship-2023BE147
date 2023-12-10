using DomainLayer.DTO;
using DomainLayer.Models;
using DomainLayer.Repository;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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


        public List<DoctorWithSpecializtionDto> GetAll(int pageNumber = 1, int pageSize = 10, string search = "")
        {
            var query = myContext.Doctors.Include(d => d.Specialization).AsQueryable();

            
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(d =>
                    d.ApplicationUser.FullName.Contains(search) ||
                    d.ApplicationUser.Email.Contains(search) ||
                    d.Specialization.NameAr.Contains(search) ||
                    d.Specialization.NameEn.Contains(search)
                );
            }

            // Pagination
            var paginatedResult = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(d => new DoctorWithSpecializtionDto
                {
                    Image = d.ApplicationUser.Image,
                    FullName = d.ApplicationUser.FullName,
                    Email = d.ApplicationUser.Email,
                    Phone = d.ApplicationUser.PhoneNumber,
                    Gender = (Gender)d.ApplicationUser.Gender,
                    SpecializationId = d.SpecializationId,
                    SpecializationNameEN = d.Specialization.NameAr,
                    SpecializationNameAR = d.Specialization.NameEn
                })
                .ToList();

            return paginatedResult;
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
                 Phone = d.ApplicationUser.PhoneNumber,
                 Gender = (Gender)d.ApplicationUser.Gender,
                 SpecializationNameEN = d.Specialization.NameAr,
                 SpecializationNameAR = d.Specialization.NameEn
             }).FirstOrDefault();
            return (DoctorWithspecialization);
        }

        public void UPDATE(string id, DoctorWithSpecializtionDto doctorDto)
        {
            Doctor oldDoctor = myContext.Doctors.Include(d => d.ApplicationUser)
                                                .Include(d => d.Specialization)
                                                .FirstOrDefault(e => e.Id == id);

            if (oldDoctor != null)
            {
                if (oldDoctor.ApplicationUser != null)
                {
                   oldDoctor.ApplicationUser.Image = doctorDto.Image;
                    oldDoctor.ApplicationUser.FullName = doctorDto.FullName;
                    oldDoctor.ApplicationUser.Email = doctorDto.Email;
                    oldDoctor.ApplicationUser.DateOfBirth = doctorDto.DateOfBirth;
                    oldDoctor.ApplicationUser.Gender = doctorDto.Gender;
                    oldDoctor.ApplicationUser.PhoneNumber = doctorDto.Phone;
                }
               

                
                    oldDoctor.SpecializationId = (int)doctorDto.SpecializationId;
                    
                

                myContext.SaveChanges(); 
            }
        }



        public void DELETE(string id)
        {
            var doctorToDelete = myContext.Doctors.Include(d => d.ApplicationUser)
                                                .Include(d => d.Specialization)
                                                .FirstOrDefault(e => e.Id == id);

            if (doctorToDelete != null)
            {
                myContext.Doctors.Remove(doctorToDelete);
                doctorToDelete.ApplicationUser.IsDeleted = true;
                myContext.SaveChanges();
             
            }

            else
            {

                throw new ArgumentException("Doctor not found");

            }
        }

        public int NumberOfDoctor()
        {
           var doctors= myContext.Doctors.Count();
            return doctors;
        }

        public List<TopDoctorsDTO> Top10Doctor()
        {
            var top10Doctors = myContext.Doctors
          .Select(d => new TopDoctorsDTO
          {
              Image = d.ApplicationUser.Image,
              FullName = d.ApplicationUser.FullName,
              Specialize=d.Specialization.NameEn,
              Requests = d.Requests.Count()
          })
          .OrderByDescending(d => d.Requests)
          .Take(10)
          .ToList();

            return top10Doctors;
        }

        public List<Top5SpecializeDTO> Top5Specialize()
        {
            var specialize=myContext.Specializations.
                Select(d => new Top5SpecializeDTO {
                
                
                NameEn=d.NameEn,
                NameAr=d.NameAr,
                Requests=d.Doctors.Count(),
                
                
                })
                 .OrderByDescending(d => d.Requests)
                 .Take(5)
                 .ToList();
            return specialize;
        }
        //patient
        public List<DoctorDto> SearchonDoctors(int pageNumber=1 , int pageSize = 10 , string search="" )
        {
            var query = myContext.Doctors
                .Include(d => d.Specialization)
                .Include(d => d.Appointments).
                AsQueryable();


            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(d =>
                    d.ApplicationUser.FullName.Contains(search) ||
                    d.ApplicationUser.Email.Contains(search) ||
                    d.Specialization.NameAr.Contains(search) ||
                    d.Specialization.NameEn.Contains(search)
                );
            }

            
            var paginatedResult = query
                 .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(d => new DoctorDto
                {
                    Image = d.ApplicationUser.Image,
                    FullName = d.ApplicationUser.FullName,
                    Email = d.ApplicationUser.Email,
                    Phone = d.ApplicationUser.PhoneNumber,
             
                    Gender = (Gender)d.ApplicationUser.Gender,
               
                    SpecializationNameEN = d.Specialization.NameAr,
                    SpecializationNameAR = d.Specialization.NameEn,
                    price=(decimal)d.Price,
                    appointment=d.Appointments.ToList()
                    
                })
                .ToList();

            return paginatedResult;
        }
    }
    }



        
   

