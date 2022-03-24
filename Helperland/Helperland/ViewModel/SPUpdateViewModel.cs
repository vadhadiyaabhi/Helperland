using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModel
{
    public class SPUpdateViewModel : UserUpdateViewModel
    {
        [Required]
        [StringLength(200)]
        [Display(Prompt = "Street Name")]
        public string AddressLine1 { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Prompt = "House Number")]
        public string AddressLine2 { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }

        public int AddressId { get; set; }

        [Required(ErrorMessage = "Before proceeding, Please enter Postal code in which your house is located")]
        [Display(Prompt = "Postal Code")]
        [RegularExpression(@"[0-9]{6}", ErrorMessage = "Enter valid Postal Code")]
        //[MinLength(6, ErrorMessage = "Enter Valid Postal Code"), MaxLength(6, ErrorMessage = "Enter Valid Postal Code")]
        public string ZipCode { get; set; }

        public bool WorksWithPet { get; set; }

        public string Avatar { get; set; }
    }
}
