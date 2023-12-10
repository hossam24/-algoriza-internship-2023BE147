using DomainLayer.Repository;
using EFLayer.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace VezeetaEndPointApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]

    public class DashBoardController : ControllerBase
    {
        private readonly IDoctorRepo doctorRepo;
        private readonly IPatientRepo patientRepo;
        private readonly IRequestRepo requestRepo;

        public DashBoardController(IDoctorRepo doctorRepo,IPatientRepo patientRepo,IRequestRepo requestRepo)
        {
            this.doctorRepo = doctorRepo;
            this.patientRepo = patientRepo;
            this.requestRepo = requestRepo;
        }

        [HttpGet("NumOfDoctors")]
        public IActionResult NumOfDoctors() { 


        return Ok(doctorRepo.NumberOfDoctor());
        
        }

        [HttpGet("Top10Doctors")]
        public IActionResult Top10Doctors()
        {


            return Ok(doctorRepo.Top10Doctor());

        }

        [HttpGet("NumOfPatient")]
        public IActionResult NumOfPatient()
        {


            return Ok(patientRepo.NumsOfPatient());

        }
        [HttpGet("NumsOfRequests")]
        public IActionResult NumOfRequests() {



            return Ok(requestRepo.NumOfRequests());
        
        }
        [HttpGet("Top5Specialize")]
        public IActionResult Top5Specialize() {



            return Ok(doctorRepo.Top5Specialize());
        
        }
    }
}
