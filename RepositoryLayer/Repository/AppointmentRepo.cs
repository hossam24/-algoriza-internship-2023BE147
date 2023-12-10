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
    public class AppointmentRepo : IAppointmentRepo
    {
        private readonly MyContext myContext;

        public AppointmentRepo(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public void add(AppointmentDTO item)
        {
          
           var doctor = myContext.Doctors
                .FirstOrDefault(d => d.Id == item.DoctorId);

            if (doctor != null)
            {

                var times = new List<Times> { new Times { Time = item.times.Time } };
                var appointment = new Appointment
                {
                   
                    DoctorId = item.DoctorId,
                   // Doctor = doctor,
                    Day = item.days,
                    times = times,

                };
                appointment.Doctor.Price= item.price;

          
                myContext.Appointments.Add(appointment);
                myContext.SaveChanges();
            }
            else
            {
                
                throw new Exception("Doctor not found");
            }
        }

        public void Delete(int appointmentId, TimeSpan time)
        {
            using (var context = new MyContext()) 
            {
                var appointment = context.Appointments
                    .Include(a => a.times)
                    .FirstOrDefault(a => a.AppointmentId == appointmentId);

                if (appointment != null)
                {
                    // Check if the time to delete is already booked
                    var isTimeBooked = appointment.times.Any(t => t.Time == time);
                    if (isTimeBooked)
                    {
                        throw new Exception("The time is already booked and cannot be deleted");
                    }

                    //remove the time from the appointment
                    var timeToRemove = appointment.times.FirstOrDefault(t => t.Time == time);
                    if (timeToRemove != null)
                    {
                        appointment.times.Remove(timeToRemove);
                    }
                    else
                    {
                        throw new Exception("The time not found in the appointment");
                    }

                  
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Appointment not found");
                }
            }
        }

        public void Update(int appointmentId, TimeSpan newTime)
        {
            using (var context = new MyContext()) 
            {
                var appointment = context.Appointments
                    .Include(a => a.times)
                    .FirstOrDefault(a => a.AppointmentId == appointmentId);

                if (appointment != null)
                {
                    
                    var isTimeAvailable = appointment.times.All(t => t.Time != newTime);
                    if (!isTimeAvailable)
                    {
                        throw new Exception("The new time is already booked");
                    }

                   
                    var oldTime = appointment.times.FirstOrDefault();
                    if (oldTime != null)
                    {
                        oldTime.Time = newTime;
                    }
                    else
                    {
                        throw new Exception("No time found to update");
                    }

                  
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Appointment not found");
                }
            }
        }
    }
}
