using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Helperland.Models
{
    [Table("FavoriteAndBlocked")]
    public partial class FavoriteAndBlocked
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TargetUserId { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsBlocked { get; set; }

        [ForeignKey(nameof(TargetUserId))]
        [InverseProperty("FavoriteAndBlockedTargetUsers")]
        public virtual User TargetUser { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("FavoriteAndBlockedUsers")]
        public virtual User User { get; set; }
    }
}
