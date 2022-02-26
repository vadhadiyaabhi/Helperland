using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModel
{
    public class UserAddressViewModel : ZipCodeViewModel
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

        [StringLength(50)]
        public string State { get; set; }

        [Required]
        [Display(Prompt = "Mobile Number")]
        [MinLength(10, ErrorMessage = "Please enter valid Mobile number"), MaxLength(10, ErrorMessage = "Please enter valid Mobile number")]
        [StringLength(20)]
        public string Mobile { get; set; }
    }
}
