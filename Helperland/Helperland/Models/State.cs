using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Helperland.Models
{
    [Table("State")]
    public partial class State
    {
        public State()
        {
            Cities = new HashSet<City>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string StateName { get; set; }

        [InverseProperty(nameof(City.State))]
        public virtual ICollection<City> Cities { get; set; }
    }
}
