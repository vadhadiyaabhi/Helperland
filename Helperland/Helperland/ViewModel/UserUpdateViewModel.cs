using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModel
{
    public class UserUpdateViewModel
    {
        [Required]
        [StringLength(100)]
        [MaxLength(30, ErrorMessage = "First name cannot exceed 30 Characters")]
        [Display(Prompt = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        [MaxLength(30, ErrorMessage = "Last name cannot exceed 30 Characters")]
        [Display(Prompt = "Last Name")]
        public string LastName { get; set; }


        [Required]
        [StringLength(20)]
        [MinLength(10, ErrorMessage = "Please enter valid Mobile number"), MaxLength(10, ErrorMessage = "Please enter valid Mobile number")]
        [Display(Prompt = "Mobile number")]
        public string Mobile { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? DateOfBirth { get; set; }

        public int? LanguageId { get; set; }

        public string Email { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime ModifiedDate
        {
            get
            { return (DateTime.Now); }
        }
    }
}
