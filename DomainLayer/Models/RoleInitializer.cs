using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class RoleInitializer
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleInitializer(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task InitializeRoles()
        {
            var roleExists = await _roleManager.RoleExistsAsync("Patient");
            if (!roleExists)
            {
                // Create the role
                var role = new IdentityRole("PATIENT");
                await _roleManager.CreateAsync(role);
            }

            // Add other roles initialization if needed
        }
    }
}
