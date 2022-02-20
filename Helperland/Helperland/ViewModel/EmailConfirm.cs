using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModel 
{
    public class EmailConfirm : AuthenticationViewModel
    {
        public int id { get; set; }
        public bool EmailSent { get; set; }
        public bool IsActive { get; set; }
        public bool  EmailConfirmed { get; set; }
        public string name { get; set; }
        public int Id { get; set; }
        public bool Invalid { get; set; }
        public bool userNotExist { get; set; }

    }
}
