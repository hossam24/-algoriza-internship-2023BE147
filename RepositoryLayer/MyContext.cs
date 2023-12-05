using DomainLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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
        public DbSet<Patient> Patients { get; set; }
        public DbSet<DiscountCode> DiscountCodes { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
             string ADMIN_ID = "76f86073-b51c-47c4-b7fa-731628055ebb";
            string ROLE_ID = "5ab58670-8727-4b67-85d5-4199912a70bf";


            //  Create an admin user
            ApplicationUser admin = new ApplicationUser
            {
                Id = ADMIN_ID,
                Email = "admin@gmail.com",
                NormalizedEmail = "admin@gmail.com".ToUpper(),
                EmailConfirmed = true,
                UserName = "admin",
                NormalizedUserName = "admin@gmail.com".ToUpper(),
                LockoutEnabled = true
            };
            var hasher = new PasswordHasher<ApplicationUser>();
            admin.PasswordHash = hasher.HashPassword(admin, password: "@Admin123");


            builder.Entity<ApplicationRole>().HasData(
     new ApplicationRole
     {
         Id = ROLE_ID,
         Name = "Admin",
         NormalizedName = "ADMIN",
         Date = DateTime.Now.ToString()
     });

            //Connect An admin to Role Admin
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                { RoleId = ROLE_ID, UserId = ADMIN_ID });



            //create static role 
            builder.Entity<ApplicationRole>().HasData(
               new ApplicationRole
               {
                   Id = Guid.NewGuid().ToString(),
                   Name = "Patient",
                   NormalizedName = "Patient",
                   Date = DateTime.Now.ToString()
               });
            builder.Entity<ApplicationRole>().HasData(
                new ApplicationRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Doctor",
                    NormalizedName = "Doctor",
                    Date = DateTime.Now.ToString()
                });  
          

        }
      

    }
}








//public async Task SeedRoles(ModelBuilder modelBuilder)
//{
//    string[] roleNames = { "Admin", "Doctor", "Patient" };

//    foreach (var roleName in roleNames)
//    {
//        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
//        {
//            Id = Guid.NewGuid().ToString(),
//            Name = roleName,
//            NormalizedName = roleName.ToUpper()
//        });
//    }

//}


//   SeedRoles(builder).GetAwaiter().GetResult();