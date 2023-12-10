using Azure.Core;
using DomainLayer.DTO;
using DomainLayer.Repository;
using EFLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace VezeetaEndPointApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IRequestRepo requestRepo;

        public BookingController(IRequestRepo requestRepo)
        {
            this.requestRepo = requestRepo;


        }
        //doctor
        [HttpGet("GetAllBookingOfThisDoctor")]
        public IActionResult GetAllBooking(string doctorId, DateTime date, int pageSize = 10, int pageNumber = 1)
        {
            var bookings = requestRepo.Bookingofboctor(doctorId,pageSize, pageNumber);
            return Ok(bookings);
        }
        //patient
        [HttpGet("GetAllBookingOfThisPatient")]
        public IActionResult GetAllbookingpatient(string id) {


            try {
                var bookings = requestRepo.BookingofPatient(id);
                return Ok(bookings);

            }
        
        catch (Exception ex) { 
            
            return BadRequest(ex.Message);  
            }
        }
     //patient
        [HttpPut("cancel booking")]
        public IActionResult cancelled(int id)
        {
            try
            {
                requestRepo.CancelBooking(id);
                 return Ok("Request cancelled successfully");
            }
            catch (Exception ex) {

                return BadRequest("Request not found ");

            }
        }

        //patient
        [HttpPost("booking")]
        public IActionResult add([FromForm]AddRequestDTO addRequestDTO) {

            try
            {
                requestRepo.ADDRequest(addRequestDTO);
                return Ok(true);
            }catch  (Exception ex) {

                return Ok(false);
            
            }
        
        }

        //for doctor
        [HttpPut("ConfirmCheckup")]
        public IActionResult confirmcheckup(int id)
        {
            try
            {
                requestRepo.confirmcheckup(id);
                return Ok("Request cancelled successfully");
            }
            catch (Exception ex)
            {

                return BadRequest("Request not found ");

            }
        }


    }
}
