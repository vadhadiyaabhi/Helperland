using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModel
{
    public class ServiceTimeConflict
    {
        public string ConflictDate { get; set; }

        public string ConflictStartTime { get; set; }

        public string ConflictEndTime { get; set; }

        public int SPId { get; set; }

        public int ServiceId { get; set; }

        public bool Conflict { get; set; }
    }
}
