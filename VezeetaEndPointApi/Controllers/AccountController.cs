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
       //patient
        [HttpPost("register")]
        public async Task<IActionResult> RegistrationAsync([FromForm]RegisterDto registerUserDto)
        {
            if (ModelState.IsValid)
            {
                
                    ApplicationUser user = new ApplicationUser();
                                      
                    user.FullName = registerUserDto.FullName;
                user.UserName = registerUserDto.FullName.ToLower();
                    user.PhoneNumber = registerUserDto.Phone;
                    user.Gender = registerUserDto.Gender;
                    user.Image = registerUserDto.Image;
                    user.Email = registerUserDto.Email;
                    user.DateOfBirth= registerUserDto.DateOfBirth;  
                    user.SecurityStamp = Guid.NewGuid().ToString();
                    user.Type = AccountType.Patient;
                    user.EmailConfirmed = true;
                    user.LockoutEnabled = true;
                    

                    IdentityResult result = await userManager.CreateAsync(user, registerUserDto.Password);








                if (result.Succeeded)
                {
                    //var role = user.Type.ToString();

                    //var roleExists = await roleManager.RoleExistsAsync(role);

                    //if (!roleExists)
                    //{
                    //    var newRole = new IdentityRole(role);
                    //    var roleCreationResult = await roleManager.CreateAsync(newRole);

                    //    if (!roleCreationResult.Succeeded)
                    //    {
                    //        return BadRequest("Role creation failed.");
                    //    }
                    //}
                    ApplicationUser createdUser = await userManager.FindByNameAsync(user.UserName);
                    var roleAssignmentResult = await userManager.AddToRoleAsync(createdUser, "Patient");

                    if (roleAssignmentResult.Succeeded)
                    {
                        return Ok("Account added successfully");
                    }
                    else
                    {
                        return BadRequest("Role assignment failed.");
                    }
                }
                else
                {
                    return BadRequest(result.Errors.FirstOrDefault()?.Description);
                }
            }

            return BadRequest(ModelState);
        }


        
        [HttpPost("login")]
       
        public async Task<IActionResult> Login([FromForm] LoginDto model)
        {
            ApplicationUser user = await userManager.FindByEmailAsync(model.Email);
            if (user != null && await userManager.CheckPasswordAsync(user, model.password))
            {


                var authClaims = new List<Claim>();

                authClaims.Add(new Claim(ClaimTypes.Name, user.Email));
                authClaims.Add( new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                //get roles
               var roleuser=  await userManager.FindByEmailAsync( user.Email );
                var roles = await userManager.GetRolesAsync(roleuser);
                foreach (var role in roles) {

                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                SecurityKey authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Secret"]));
                JwtSecurityToken token = new JwtSecurityToken(
                    issuer: config["JWT:ValidIssuer"],
                    audience: config["JWT:ValidAudience"],
                    expires: DateTime.UtcNow.AddHours(4),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                var tokenHandler = new JwtSecurityTokenHandler();
                var serializedToken = tokenHandler.WriteToken(token);

                return Ok(new
                {
                    token = serializedToken,
                    expiration = token.ValidTo
                });
            }

            return Unauthorized("Invalid credentials. Please check your email and password.");
        }




    }
}
