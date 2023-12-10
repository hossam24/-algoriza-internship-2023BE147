using DomainLayer.DTO;
using DomainLayer.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VezeetaEndPointApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //doctor
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentRepo appointmentrepo;

        public AppointmentController(IAppointmentRepo appointmentrepo)
        {
            this.appointmentrepo = appointmentrepo;
        }
        [HttpPost("ADDAppointment")]
        public IActionResult add([FromForm]AppointmentDTO item) {

            try { 
            
            
            appointmentrepo.add(item);
                return Ok("Appointment added successfully");


            }
            catch (Exception ex) {

                return BadRequest("Doctor not found");

            }
        
        
        
        
        
        }

        [HttpPut("UpdateAppointment")]
        public IActionResult update(int id ,TimeSpan time) {

            try
            {


                appointmentrepo.Update(id,time);
                return Ok("Appointment added successfully");


            }
            catch (Exception ex)
            {

                return BadRequest("Doctor not found");

            }




        }



    }
}
