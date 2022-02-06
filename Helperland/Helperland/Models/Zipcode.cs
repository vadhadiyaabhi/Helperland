using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Helperland.Models
{
    [Table("Zipcode")]
    public partial class Zipcode
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string ZipcodeValue { get; set; }
        public int CityId { get; set; }

        [ForeignKey(nameof(CityId))]
        [InverseProperty("Zipcodes")]
        public virtual City City { get; set; }
    }
}
