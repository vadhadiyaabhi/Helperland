using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModel
{
    public class ZipCodeViewModel
    {
        [Required]
        [Display(Prompt = "Postal Code")]
        [MinLength(6, ErrorMessage = "Enter Valid Postal Code"), MaxLength(6, ErrorMessage = "Enter Valid Postal Code")]
        public string ZipCode { get; set; }
    }
}
