using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModel
{
    public class NewReqEmailModel
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public DateTime DateTime { get; set; }

        public string ExtraServices { get; set; }

        public string SPName { get; set; }

        public decimal TotalAmount { get; set; }

        public int SPId { get; set; }

        public int ServiceId { get; set; }
    }
}
