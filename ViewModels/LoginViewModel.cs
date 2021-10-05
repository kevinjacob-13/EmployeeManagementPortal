using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DotNetAssignment.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username can't be blank")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password can't be blank")]
        public string Password { get; set; }
    }
}



