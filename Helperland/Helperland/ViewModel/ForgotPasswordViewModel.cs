using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModel
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [Display(Name = "New Password")]
        [MaxLength(14, ErrorMessage = "Password cannot exceed 14 Characters"), MinLength(6, ErrorMessage = "Password must contain atleast 6 Characters"),
            RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{6,}", ErrorMessage = "Password should be combination of Uppercase, Lowercase and Digit")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Password and ConfirmPassword must match")]
        public string ConfirmPassword { get; set; }

        public string Token { get; set; }
    }
}
