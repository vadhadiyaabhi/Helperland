using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Helperland.Models
{
    [Table("ServiceRequest")]
    public partial class ServiceRequest
    {
        public ServiceRequest()
        {
            Ratings = new HashSet<Rating>();
            ServiceRequestAddresses = new HashSet<ServiceRequestAddress>();
            ServiceRequestExtras = new HashSet<ServiceRequestExtra>();
        }

        [Key]
        public int ServiceRequestId { get; set; }
        public int UserId { get; set; }
        public int ServiceId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ServiceStartDate { get; set; }
        [Required]
        [StringLength(10)]
        public string ZipCode { get; set; }
        [Column(TypeName = "decimal(8, 2)")]
        public decimal? ServiceHourlyRate { get; set; }
        public double ServiceHours { get; set; }
        public double? ExtraHours { get; set; }
        [Column(TypeName = "decimal(8, 2)")]
        public decimal SubTotal { get; set; }
        [Column(TypeName = "decimal(8, 2)")]
        public decimal? Discount { get; set; }
        [Column(TypeName = "decimal(8, 2)")]
        public decimal TotalCost { get; set; }
        [StringLength(500)]
        public string Comments { get; set; }
        [StringLength(50)]
        public string PaymentTransactionRefNo { get; set; }
        public bool PaymentDue { get; set; }
        public int? ServiceProviderId { get; set; }
        [Column("SPAcceptedDate", TypeName = "datetime")]
        public DateTime? SpacceptedDate { get; set; }
        public bool HasPets { get; set; }
        public int? Status { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        [Column(TypeName = "decimal(8, 2)")]
        public decimal? RefundedAmount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Distance { get; set; }
        public bool? HasIssue { get; set; }
        public bool? PaymentDone { get; set; }
        public Guid? RecordVersion { get; set; }

        [ForeignKey(nameof(ServiceProviderId))]
        [InverseProperty("ServiceRequestServiceProviders")]
        public virtual User ServiceProvider { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("ServiceRequestUsers")]
        public virtual User User { get; set; }
        [InverseProperty(nameof(Rating.ServiceRequest))]
        public virtual ICollection<Rating> Ratings { get; set; }
        [InverseProperty(nameof(ServiceRequestAddress.ServiceRequest))]
        public virtual ICollection<ServiceRequestAddress> ServiceRequestAddresses { get; set; }
        [InverseProperty(nameof(ServiceRequestExtra.ServiceRequest))]
        public virtual ICollection<ServiceRequestExtra> ServiceRequestExtras { get; set; }
    }
}
