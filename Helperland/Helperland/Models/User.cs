using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Helperland.Models
{
    [Table("User")]
    [Index(nameof(Email), Name = "Unique_Email", IsUnique = true)]
    public partial class User
    {
        public User()
        {
            FavoriteAndBlockedTargetUsers = new HashSet<FavoriteAndBlocked>();
            FavoriteAndBlockedUsers = new HashSet<FavoriteAndBlocked>();
            RatingRatingFromNavigations = new HashSet<Rating>();
            RatingRatingToNavigations = new HashSet<Rating>();
            ServiceRequestServiceProviders = new HashSet<ServiceRequest>();
            ServiceRequestUsers = new HashSet<ServiceRequest>();
            UserAddresses = new HashSet<UserAddress>();
        }

        [Key]
        public int UserId { get; set; }
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(100)]
        public string LastName { get; set; }
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(100)]
        public string Password { get; set; }
        [Required]
        [StringLength(20)]
        public string Mobile { get; set; }
        public int UserTypeId { get; set; }
        public int? Gender { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateOfBirth { get; set; }
        [StringLength(200)]
        public string UserProfilePicture { get; set; }
        public bool IsRegisteredUser { get; set; }
        [StringLength(200)]
        public string PaymentGatewayUserRef { get; set; }
        [StringLength(20)]
        public string ZipCode { get; set; }
        public bool WorksWithPets { get; set; }
        public int? LanguageId { get; set; }
        public int? NationalityId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }
        public bool IsApproved { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int? Status { get; set; }
        [StringLength(100)]
        public string BankTokenId { get; set; }
        [StringLength(50)]
        public string TaxNo { get; set; }
        [Column("token")]
        [StringLength(256)]
        public string Token { get; set; }
        [Column("token_expired")]
        public bool? TokenExpired { get; set; }

        [InverseProperty(nameof(FavoriteAndBlocked.TargetUser))]
        public virtual ICollection<FavoriteAndBlocked> FavoriteAndBlockedTargetUsers { get; set; }
        [InverseProperty(nameof(FavoriteAndBlocked.User))]
        public virtual ICollection<FavoriteAndBlocked> FavoriteAndBlockedUsers { get; set; }
        [InverseProperty(nameof(Rating.RatingFromNavigation))]
        public virtual ICollection<Rating> RatingRatingFromNavigations { get; set; }
        [InverseProperty(nameof(Rating.RatingToNavigation))]
        public virtual ICollection<Rating> RatingRatingToNavigations { get; set; }
        [InverseProperty(nameof(ServiceRequest.ServiceProvider))]
        public virtual ICollection<ServiceRequest> ServiceRequestServiceProviders { get; set; }
        [InverseProperty(nameof(ServiceRequest.User))]
        public virtual ICollection<ServiceRequest> ServiceRequestUsers { get; set; }
        [InverseProperty(nameof(UserAddress.User))]
        public virtual ICollection<UserAddress> UserAddresses { get; set; }
    }
}
