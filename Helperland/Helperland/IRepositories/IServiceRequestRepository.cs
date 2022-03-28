using Helperland.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.IRepositories
{
    public interface IServiceRequestRepository
    {
        public City GetCityName(string PostalCode);

        public bool IsPostalCodeAvailable(string postalCode);

        public IEnumerable<UserAddress> GetUserAddresses(int id, string zipCode);

        public IEnumerable<UserAddress> GetUserAddresses(int id);

        public int AddServiceRequest(ServiceRequest serviceRequest);

        public UserAddress GetUserAddress(int addId);

        public bool AddServiceReqAddress(ServiceRequestAddress serviceAddress);

        public int AddNewAddress(UserAddress userAddress);

        public IEnumerable<User> GetUserWithZipCode(string zipCode, bool worksWithPet);
    }
}
