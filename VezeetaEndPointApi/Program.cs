
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

namespace VezeetaEndPointApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(
            //    options=>
            //{
            //    options.AddSecurityDefinition("oauth2", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            //    {

            //        In=Microsoft.OpenApi.Models.ParameterLocation.Header,
            //        Name="Authorization",
            //        Type=Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,



            //    });
            //    options.OperationFilter<SecurityRequirementsOperationFilter>();
                
                
                
                
                
            
            
            
            
            
            //}
            
            
            
            
            );
                
            
           
           
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


            //JWT Configurations


            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                        ValidAudience = builder.Configuration["JWT:ValidAudiance"],
                    IssuerSigningKey =new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))

                    };
                });

           





            var app = builder.Build();
            using var scope = app.Services.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
           
        

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