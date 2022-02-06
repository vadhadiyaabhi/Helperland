using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModel
{
    public class AuthenticationViewModel
    {
        [Required]
        [Display(Prompt = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Prompt = "Password")]
        public string password { get; set; }
    }
}
