using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModel
{
    public class EmailConfig
    {
        public string SenderAddress { get; set; }
        public string SenderDisplayName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int port { get; set; }

        public bool IsBodyHtml { get; set; }
        public bool EnableSSL { get; set; }
        public bool UserDefaultCredentials { get; set; }
    }
}
