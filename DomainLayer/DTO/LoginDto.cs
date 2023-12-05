﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.DTO
{
    public class LoginDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string password { get; set; }
    }
}
