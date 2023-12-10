using Azure.Core;
using DomainLayer.DTO;
using DomainLayer.Repository;
using EFLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
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
        [Authorize(Roles = "Doctor")]

        public IActionResult GetAllBooking( DateTime date, int pageSize = 10, int pageNumber = 1)
        {
            var bookings = requestRepo.Bookingofboctor(pageSize, pageNumber);
            return Ok(bookings);
        }
        //patient
        [HttpGet("GetAllBookingOfThisPatient")]
        [Authorize(Roles = "Patient")]

        public IActionResult GetAllbookingpatient() {


            try {
                var bookings = requestRepo.BookingofPatient();
                return Ok(bookings);

            }
        
        catch (Exception ex) { 
            
            return BadRequest(ex.Message);  
            }
        }
     //patient
        [HttpPut("cancel booking")]
        [Authorize(Roles = "Patient")]

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
        [Authorize(Roles = "Patient")]

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
        [Authorize(Roles = "Doctor")]

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
