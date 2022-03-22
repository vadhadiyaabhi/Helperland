using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModel
{
    public class EditServiceViewModel
    {
        [Required]
        public int ServiceId { get; set; }

        [Required]
        public int AddressId { get; set; }

        [Required]
        public string Date { get; set; }

        [Required]
        public string Time { get; set; }

        [Required]
        [MaxLength(30)]
        public string AddressLine1 { get; set; }

        [Required]
        [MaxLength(30)]
        public string AddressLine2 { get; set; }

        [Required]
        [Remote(action: "SPAvailabilityInZipCode", controller: "Admin")]
        [Display(Prompt = "Postal Code")]
        [RegularExpression(@"[0-9]{6}", ErrorMessage = "Enter valid Postal Code")]
        public string ZipCode { get; set; }

        [Required]
        [MaxLength(30)]
        public string City { get; set; }

        [Required]
        [MaxLength(200)]
        public string Comment { get; set; }
    }
}
