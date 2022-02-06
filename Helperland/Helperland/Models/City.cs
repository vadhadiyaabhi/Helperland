using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Helperland.Models
{
    [Table("City")]
    public partial class City
    {
        public City()
        {
            Zipcodes = new HashSet<Zipcode>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string CityName { get; set; }
        public int StateId { get; set; }

        [ForeignKey(nameof(StateId))]
        [InverseProperty("Cities")]
        public virtual State State { get; set; }
        [InverseProperty(nameof(Zipcode.City))]
        public virtual ICollection<Zipcode> Zipcodes { get; set; }
    }
}
