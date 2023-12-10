using DomainLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection.Emit;

namespace EFLayer
{
    public class MyContext : IdentityDbContext<ApplicationUser>
    {
        public MyContext() : base()
        { }
        public MyContext(DbContextOptions<MyContext> options)
        : base(options)
        { }
        public DbSet<Doctor> Doctors { get; set; }
       
        public DbSet<DiscountCode> DiscountCodes { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
             string ADMIN_ID = "76f86073-b51c-47c4-b7fa-731628055ebb";
           


             //  Create an admin user
            ApplicationUser admin = new ApplicationUser
            {
                Id = ADMIN_ID,
                Email = "admin@gmail.com",
                NormalizedEmail = "admin@gmail.com".ToUpper(),
                EmailConfirmed = true,
                UserName = "admin",
                NormalizedUserName = "admin@gmail.com".ToUpper(),
                LockoutEnabled = true,
                Type=AccountType.Admin
                
            };
            var hasher = new PasswordHasher<ApplicationUser>();
            admin.PasswordHash = hasher.HashPassword(admin, password: "@Admin123");
            builder.Entity<ApplicationUser>().HasData(admin);

         

        }


    }
}







