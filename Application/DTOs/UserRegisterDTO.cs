using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class UserRegisterDTO
    {
        public string UserName { get; set; }

        public string Password { get; set; }


        [Compare("Password", ErrorMessage = "Password and Re-password are different")]
        public string RePassword { get; set; }


        public int Phone { get; set; }
        public string Email { get; set; }

    }
}
