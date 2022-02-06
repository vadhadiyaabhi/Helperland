using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Helperland.Models
{
    [Keyless]
    [Table("Test")]
    public partial class Test
    {
        public int TestId { get; set; }
        [StringLength(50)]
        public string TestName { get; set; }
    }
}
