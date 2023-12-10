
using Microsoft.EntityFrameworkCore;
using EFLayer;
using DomainLayer.Repository;
using EFLayer.Repository;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DomainLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using VezeetaEndPointApi.Filters;

namespace VezeetaEndPointApi
{
    public class Program
    {
        /// algriza-internship-2023BE147
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

   //**Important** // this config for swagger to enable autherization but if there any problem please test it in Postman it work done there
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Enter the JWT token in the format 'Bearer {token}'",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new List<string>()
        }
    });

                options.OperationFilter<AddAuthorizationHeaderOperationFilter>();
            });




            //add DB Context 
            builder.Services.AddDbContext<MyContext>(o =>
          o.UseSqlServer(builder.Configuration.GetConnectionString("conn")));

            //configure Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
          .AddEntityFrameworkStores<MyContext>()
          .AddDefaultTokenProviders();
            //Register dependency injection
            builder.Services.AddScoped(typeof(IBaseRepo<>), typeof(BaseRepo<>));
            builder.Services.AddScoped<UserManager<ApplicationUser>>();
            builder.Services.AddScoped<SignInManager<ApplicationUser>>();
            builder.Services.AddScoped<RoleManager<IdentityRole>>();
            builder.Services.AddScoped<RoleInitializer>();
            builder.Services.AddScoped<IDoctorRepo,DoctorRepo>();
            builder.Services.AddScoped<IRequestRepo,RequestRepo>();
            builder.Services.AddScoped<IPatientRepo,PatientRepo>();
            builder.Services.AddScoped<IDiscountRepo,DiscountCodeRepo>();
            builder.Services.AddScoped<IAppointmentRepo,AppointmentRepo>();


            //JWT Configurations using JWT bouns


            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
            ValidAudience = builder.Configuration["JWT:ValidAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
        };
    });






            //add My Roles 
            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var roles = new[] { "Admin", "Doctor", "Patient" };
                foreach (var role in roles)
                {

                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));

                    }
                }
            }
            using (var scope = app.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                string email = "admin@gmail.com";

                var user = await userManager.FindByEmailAsync(email);

                if (user != null)
                {
                    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                    var roleExists = await roleManager.RoleExistsAsync("Admin");

                    if (!roleExists)
                    {
                        
                        await roleManager.CreateAsync(new IdentityRole("Admin"));
                    }

                  
                    await userManager.AddToRoleAsync(user, "Admin");
                }
                else
                {
                    
                    Console.WriteLine("User not found with the specified email.");
                }
            }



            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();   
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors(options =>
            {

                options.AllowAnyHeader();
                options.AllowAnyMethod();
                options.AllowAnyOrigin();

            });


            app.MapControllers();

            app.Run();
        }
       

    }
}