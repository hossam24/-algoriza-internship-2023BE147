using DomainLayer.Models;
using DomainLayer.Repository;
using EFLayer.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace VezeetaEndPointApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]

    public class PatientController : ControllerBase
    {
        private readonly IPatientRepo patientRepo;
        private readonly UserManager<ApplicationUser> userManager;

        public PatientController(IPatientRepo patientRepo,UserManager<ApplicationUser> userManager)
        {
            this.patientRepo = patientRepo;
            this.userManager = userManager;
        }

        [HttpGet]

        public IActionResult GetAll()
        {
            
            var patiens=patientRepo.GetAll();


            return Ok(patiens);
        }


        [HttpGet("{id}")]

        public IActionResult GetById([FromRoute] string id)
        {

            var patient = patientRepo.GetById(id);
            if (patient != null)
            {
                return Ok(patient);


            }
            return NotFound();
        }
    }
}
