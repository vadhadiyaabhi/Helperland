using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModel
{
    public class UserAddressViewModel
    {
        [Required]
        [StringLength(200)]
        public string AddressLine1 { get; set; }

        [StringLength(200)]
        public string AddressLine2 { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }

        [StringLength(50)]
        public string State { get; set; }

        [Required]
        [Display(Prompt = "Postal Code")]
        [MinLength(6, ErrorMessage = "Enter Valid Postal Code"), MaxLength(6, ErrorMessage = "Enter Valid Postal Code")]
        public int ZipCode { get; set; }

        [StringLength(20)]
        public string Mobile { get; set; }
    }
}
