using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

#nullable disable

namespace Helperland.Models
{
    [Index(nameof(Email), Name = "Unique_Email", IsUnique = true)]
    public partial class UserViewModel
    {

        [Key]
        public int UserId { get; set; }



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
        [StringLength(100)]
        [EmailAddress]
        [Remote(action: "IsEmailInUse", controller: "Register")]
        [Display(Prompt = "Email address")]
        public string Email { get; set; }

        [Required]
        [MaxLength(14, ErrorMessage = "Password cannot exceed 14 Characters"), MinLength(6, ErrorMessage = "Password must contain atleast 6 Characters"),
            RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{6,}", ErrorMessage ="Password should be combination of Uppercase, Lowercase and Digit")]
        [Display(Prompt = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage ="Password and Confirm Password must match")]
        [Display(Prompt = "Confirm Password")]
        [Required]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }


        [Required]
        [StringLength(20)]
        [MinLength(10, ErrorMessage = "Please enter valid Mobile number"), MaxLength(10, ErrorMessage ="Please enter valid Mobile number")]
        [Display(Prompt = "Mobile number")]
        public string Mobile { get; set; }

        public int UserTypeId { get; set; }

        public int? Gender { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? DateOfBirth { get; set; }

        [StringLength(200)]
        public string UserProfilePicture { get; set; }

        [StringLength(20)]
        public string ZipCode { get; set; }

        public bool WorksWithPets { get; set; }

        public int? LanguageId { get; set; }

        public int? NationalityId { get; set; }

        public int? Status { get; set; }

      
    }
}
