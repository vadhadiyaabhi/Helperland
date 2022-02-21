using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.IRepositories
{
    public interface IServiceRequestRepository
    {
        public string GetCityName(string PostalCode);

        public bool IsPostalCodeAvailable(string postalCode);
    }
}
