using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DomainLayer.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AccountType
    {
        Admin,
        Doctor,
        Patient
    }
    [JsonConverter(typeof(JsonStringEnumConverter))]

    public enum Gender
    {
        Female = 0,
        Male = 1
    }
    [JsonConverter(typeof(JsonStringEnumConverter))]

    public enum DiscountType
    {
        Percentage,
        Value
    }
    [JsonConverter(typeof(JsonStringEnumConverter))]

    public enum RequestStatus
    {
        Pending,
        Completed,
        Cancelled
    }
    [JsonConverter(typeof(JsonStringEnumConverter))]

    public enum Days
    {
        Saturday,
        Sunday,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday
    }

    public class ApplicationUser: IdentityUser
    {
        public string? Image { get; set; }
        public string ?FullName { get; set; }
        public string ?Email { get; set; }
        public string ?Phone { get; set; }
        public Gender ?Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public AccountType Type { get; set; }
        public Doctor? DoctorProfile { get; set; } 
        public Patient? PatientProfile { get; set; }
        public string? RefreshToken { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }



    }

    public class UserRole : IdentityUserRole<string>
    {
        public virtual ApplicationRole Role { get; set; }
    }

    public class ApplicationRoleClaim : IdentityRoleClaim<string>
    {
        public virtual ApplicationRole Role { get; set; }
    }

    public class ApplicationRole : IdentityRole<string>
    {
        public string? Date { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }

}
