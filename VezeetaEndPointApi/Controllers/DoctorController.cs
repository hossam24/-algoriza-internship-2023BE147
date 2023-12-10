using DomainLayer.DTO;
using DomainLayer.Models;
using DomainLayer.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.Text;
using EFLayer.Repository;

namespace VezeetaEndPointApi.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDoctorRepo doctorRepo;
        private readonly IBaseRepo<Doctor> baseRepo;
        private readonly IBaseRepo<Specialization> specializationrepo;
        private readonly IRequestRepo requestRepo;

        public DoctorController(RoleManager<IdentityRole> roleManager,UserManager<ApplicationUser> userManager, IDoctorRepo doctorRepo, IBaseRepo<Doctor> baseRepo, IBaseRepo<Specialization> specializationrepo,IRequestRepo requestRepo)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.doctorRepo = doctorRepo;
            this.baseRepo = baseRepo;
            this.specializationrepo = specializationrepo;
            this.requestRepo = requestRepo;
        }

       
        //Get all doctors with paggentaion
        [HttpGet]
      
        public IActionResult GetAll(int pageNumber = 1, int pageSize = 10, string search = "")
        {
            
            var doctors = doctorRepo.GetAll(pageNumber, pageSize, search);

            return Ok(doctors);
        }


        //Get Doctor By Id
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute]string id)
        {
        
            var doctor = doctorRepo.GetById(id);
            if (doctor!=null) {
                return Ok(doctor);


            }
            return NotFound();

        }
        
        //Add new Doctor with sending email and password bouns
        [HttpPost]
        public async Task<IActionResult> Add([FromForm]DoctorWithSpecializtionDto doctordto) {
            var specialization = specializationrepo.GettById((int)doctordto.SpecializationId);
            if (specialization == null)
            {
                return BadRequest("Invalid specialization ID");
            }
            if (!ModelState.IsValid) {

            return BadRequest(ModelState);  
            
            }
            string password = GenerateRandomPassword();
            var user = new ApplicationUser
            {
                Image = doctordto.Image,
                FullName = doctordto.FullName,
                Email = doctordto.Email,
                PhoneNumber = doctordto.Phone,
                Gender = doctordto.Gender,
                DateOfBirth = doctordto.DateOfBirth,
                Type = AccountType.Doctor,
                LockoutEnabled = true,
                EmailConfirmed = true,
                UserName = GenerateUsername(doctordto.FullName),
            };
            var result = await userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                var doctor = new Doctor
                {
                    //DoctorId = Guid.NewGuid().ToString(),
                    SpecializationId = (int)doctordto.SpecializationId,
                    ApplicationUser = user 
                };

                baseRepo.ADD(doctor);

                string username = user.UserName;
                

               // SendEmail(doctordto.Email, username, password);
                Console.WriteLine($"Generated Password: {password}");
              //  var role = user.Type.ToString();

                // Check if the role exists, and create it if it doesn't
                //var roleExists = await roleManager.RoleExistsAsync(role);
                //if (!roleExists)
                //{
                //    var newRole = new IdentityRole(role);
                //    await roleManager.CreateAsync(newRole);
                //}

                // Assign the role to the user
                await userManager.AddToRoleAsync(user, "Doctor");

                return Ok("Doctor added successfully");
            }
            else
            {
               
                return BadRequest(result.Errors);
            }


        }
        
        //Bonus For sending email methods

        private string GenerateUsername(string fullName)
        {
            string[] nameParts = fullName.Split(' ');
            StringBuilder usernameBuilder = new StringBuilder();

            foreach (string namePart in nameParts)
            {
                if (!string.IsNullOrEmpty(namePart))
                {
                    usernameBuilder.Append(namePart[0].ToString().ToLower());
                }
            }

            return usernameBuilder.ToString();
        }


        private string GenerateRandomPassword()
        {
            string alphanumeric = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string specialChars = "!@#$%^&*()-_=+";

            Random random = new Random();

            string password = "";
            password += alphanumeric[random.Next(alphanumeric.Length)]; // Add at least one uppercase letter
            password += specialChars[random.Next(specialChars.Length)]; // Add at least one non-alphanumeric character

            // Generate the rest of the password
            for (int i = 0; i < 6; i++) // Considering a total of 8 characters for password
            {
                password += alphanumeric[random.Next(alphanumeric.Length)];
            }

            return password;
        }


        private void SendEmail(string email, string username, string password)
        {
            string senderEmail = "jossaf168@gmail.com";
            string senderPassword = "hohohmooo22";

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail, senderPassword)
            };

            MailMessage mailMessage = new MailMessage(senderEmail, email)
            {
                Subject = "Welcome to the platform!",
                Body = $"Hello,\n\nYour username is: {username}\nYour password is: {password}\n\nWelcome aboard!"
            };

            client.Send(mailMessage);
        }



        //Edit the Doctors
        [HttpPut("{id}")]
        public IActionResult Edit([FromRoute]string id, [FromForm] DoctorWithSpecializtionDto doctorDto)
        {
            
                doctorRepo.UPDATE(id, doctorDto);
                return Ok(true);
     
        }
    

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute]string id)
        {
            bool hasRequests = requestRepo.HasRequests(id);


            if (hasRequests)
            {
                return BadRequest("Cannot delete the doctor as there are associated requests.");
            }

            doctorRepo.DELETE(id);

            return Ok(true);
        }


        [HttpGet("SearchOnDoctors")]
        public IActionResult SearchOnDoctor(int pageNumber=1 , int pageSize=10, string search = "")
        {

            var doctors = doctorRepo.SearchonDoctors(pageNumber, pageSize, search);

            return Ok(doctors);
        }



      


    }
}
