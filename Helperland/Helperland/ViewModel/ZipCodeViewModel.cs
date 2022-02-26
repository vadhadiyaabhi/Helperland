using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModel
{
    public class ZipCodeViewModel
    {
        [Required(ErrorMessage = "Before proceeding, Please enter Postal code in which your house is located")]
        [Display(Prompt = "Postal Code")]
        [RegularExpression(@"[0-9]{6}", ErrorMessage = "Enter valid Postal Code")]
        //[MinLength(6, ErrorMessage = "Enter Valid Postal Code"), MaxLength(6, ErrorMessage = "Enter Valid Postal Code")]
        public string ZipCode { get; set; }
    }
}
