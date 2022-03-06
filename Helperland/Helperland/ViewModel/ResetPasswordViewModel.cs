using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModel
{
    public class ResetPasswordViewModel : ForgotPasswordViewModel
    {
#nullable enable
        new public string? Token { get; set; }
#nullable disable

        [Required]
        [Display(Name = "Old Password")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        
    }
}
