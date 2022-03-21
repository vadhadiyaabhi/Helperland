using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModel
{
    public class FavouriteProsViewModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int TargetUserId { get; set; }

        public bool IsBlocked { get; set; }

        public bool IsFavourite { get; set; }

        public string TargetUserName { get; set; }

        public decimal Ratings { get; set; }

        public int Cleanings { get; set; }
    }
}
