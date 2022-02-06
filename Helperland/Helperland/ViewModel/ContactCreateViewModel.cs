using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Helperland.ViewModel
{
    public class ContactCreateViewModel
    {

        [Required]
        [MaxLength(20, ErrorMessage ="Name cannot exceed 20 characters")]
        [Display(Prompt = "First Name")]
        public string firstname { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Name cannot exceed 20 characters")]
        [Display(Prompt = "Last Name")]
        public string lastname { get; set; }

        [Required]
        [StringLength(200)]
        [EmailAddress]
        [Display(Prompt = "Email address")]
        public string Email { get; set; }

        [StringLength(500)]
        public string Subject { get; set; }
        [Required]
        [StringLength(20)]
        [MinLength(10, ErrorMessage = "Invalid Phone Number"), MaxLength(10, ErrorMessage ="Invalid Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
        public string Message { get; set; }
        public IFormFile Photo { get; set; }
        public string PhotoPath { get; set; }

        public int? submitted { get; set; }
    }
}
