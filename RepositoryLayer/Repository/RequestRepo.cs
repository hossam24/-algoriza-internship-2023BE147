using Azure.Core;
using DomainLayer.DTO;
using DomainLayer.Models;
using DomainLayer.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Request = DomainLayer.Models.Request;

namespace EFLayer.Repository
{
    public class RequestRepo : IRequestRepo
    {
        private readonly MyContext myContext;

        public RequestRepo(MyContext myContext)
        {
            this.myContext = myContext;
        }

        //calculate Age of patient
        public static int CalculateAge(DateTime? dateOfBirth)
        {
            if (!dateOfBirth.HasValue)
            {
                return 0;
            }

            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Value.Year;

            if (dateOfBirth.Value.Date > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }
      
        
        //if doctor has requests
        public bool HasRequests(string doctorId)
        {
            bool hasRequests = myContext.Requests.Any(r => r.DoctorId == doctorId);

            return hasRequests;
        }
        
        //nums of requests in detailes
        public RequestCountDto NumOfRequests()
        {
            var totalRequests = myContext.Requests.Count();
            var pendingRequests = myContext.Requests.Count(r => r.Status == RequestStatus.Pending);
            var completedRequests = myContext.Requests.Count(r => r.Status == RequestStatus.Completed);
            var cancelledRequests = myContext.Requests.Count(r => r.Status == RequestStatus.Cancelled);

            return new RequestCountDto
            {
                TotalRequests = totalRequests,
                PendingRequests = pendingRequests,
                CompletedRequests = completedRequests,
                CancelledRequests = cancelledRequests
            };

        }

        public List<RequestPatientDTO> Bookingofboctor(int pageSize, int pageNumber)
        {
            var bookings = myContext.Requests
                .Include(r => r.User)
                .Include(r => r.Appointment)
                    .ThenInclude(r => r.times)
              
                .OrderByDescending(r => r.Appointment.Day)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(r => new RequestPatientDTO
                {
                    PatientName = r.User.FullName,
                    Image = r.User.Image,
                    age = CalculateAge(r.User.DateOfBirth),
                    gender = (Gender)r.User.Gender,
                    phone = r.User.PhoneNumber,
                    Email = r.User.Email,
                    day = r.Appointment.Day,
                    time = r.Appointment.times.Select(t => t.Time)
                })
                .ToList();

            return bookings;
        }

        public List<RequestDto> BookingofPatient()
        {
            var bookings = myContext.Requests
                .Include(r => r.User)
                .Include(r => r.Appointment)
                    .ThenInclude(r => r.times)
               
                .OrderByDescending(r => r.Appointment.Day)
                .Select(r => new RequestDto
                {
                    DoctorImage = r.User.Image,
                    DoctorName = r.User.FullName,
                    SpecializationName =r.doctor.Specialization.NameEn,
                    FinalPrice = (decimal)r.doctor.Price,
                    Status = r.Status,
                    DiscoundCode=r.DiscountCode.Code,
                    day = r.Appointment.Day,
                    time = r.Appointment.times.Select(t => t.Time)
                })
                .ToList();

            return bookings;
        }
        
        public void CancelBooking(int id)
        {
            var request = myContext.Requests.FirstOrDefault(r => r.RequestId == id);


                request.Status = RequestStatus.Cancelled;
                myContext.SaveChanges();

             
            
          
        }
        //patient add booking
        public void ADDRequest(AddRequestDTO item)
        {
            Request request = new DomainLayer.Models.Request
            {
                 DiscountCodeId= item.DiscoundCode,
                AppointmentId = (int)item.appointmentId,
                
           

            };


            myContext.Requests.Add(request);
            myContext.SaveChanges();
        }

        public void confirmcheckup(int id)
        {
            var request = myContext.Requests.FirstOrDefault(r => r.RequestId == id);
            request.Status=RequestStatus.Completed;
            myContext.SaveChanges();
        }
    }
}
