﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.DTOs.Auth
{
    public class RegisterDTO
    {
        [Required(ErrorMessage ="Email Is Required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Display Name Is Required")]
        public string DisplayName { get; set; }

        [Required(ErrorMessage = "Phone Number Is Required")]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Password Is Required")]
        public string Password { get; set; }
    }
}