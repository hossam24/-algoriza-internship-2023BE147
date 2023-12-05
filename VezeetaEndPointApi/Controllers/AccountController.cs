using DomainLayer.DTO;
using DomainLayer.Models;
using EFLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace VezeetaEndPointApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly MyContext myContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration config;

        public AccountController(RoleManager<IdentityRole> roleManager,MyContext myContext,UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager ,IConfiguration config)
        {
            this.roleManager = roleManager;
            this.myContext = myContext;
            this.userManager = userManager;
            this.config = config;
        }
       
        [HttpPost("register")]
        public async Task<IActionResult> RegistrationAsync(RegisterDto registerUserDto)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                   
                    UserName = registerUserDto.UserName,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    Email = registerUserDto.Email,
                    Type = registerUserDto.AccountType,
                    EmailConfirmed = true,
                    NormalizedEmail = registerUserDto.Email.ToUpper(),
                    NormalizedUserName = registerUserDto.Email.ToUpper(),
                    LockoutEnabled = true
                };
                //var hasher = new PasswordHasher<ApplicationUser>();
                //user.PasswordHash = hasher.HashPassword(user, registerUserDto.Password);
                //builder.Entity<ApplicationUser>().HasData(user);

                IdentityResult result = await userManager.CreateAsync(user, registerUserDto.Password);

                switch (registerUserDto.AccountType)
                {
                    case AccountType.Patient:
                        var patientDetails = new Patient
                        {
                            ApplicationUser = user,
                           PatientId = Guid.NewGuid().ToString()
                        };

                        // Add patient to Patients table
                        myContext.Patients.Add(patientDetails);
                        break;
                    default:
                        return BadRequest("Invalid user type");
                }



                // await myContext.SaveChangesAsync();

                if (result.Succeeded)
                {
                    var role = registerUserDto.AccountType.ToString();

                    // Check if the role exists, and create it if it doesn't
                    var roleExists = await roleManager.RoleExistsAsync(role);
                    if (!roleExists)
                    {
                        var newRole = new IdentityRole(role);
                        await roleManager.CreateAsync(newRole);
                    }

                    // Assign the role to the user
                    await userManager.AddToRoleAsync(user, role);

                    return Ok("Account added successfully");
                }

                return BadRequest(result.Errors.FirstOrDefault());
            }

            return BadRequest(ModelState);
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginDto model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user != null && await userManager.CheckPasswordAsync(user, model.password))
            {
               

                var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Secret"]));
                var token = new JwtSecurityToken(
                    issuer: config["JWT:ValidIssuer"],
                    audience: config["JWT:ValidAudience"],
                    expires: DateTime.UtcNow.AddHours(1),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }

            return Unauthorized("Invalid credentials. Please check your email and password.");
        }




    }
}
