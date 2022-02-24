using Helperland.IRepositories;
using Helperland.Models;
using Helperland.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Implementations
{
    public class ServiceRequestImplementation : IServiceRequestRepository
    {
        private readonly AppDbContext dbContext;

        public ServiceRequestImplementation(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public string GetCityName(string postalCode)
        {
            return (from zipCode in dbContext.Zipcodes
                    where zipCode.ZipcodeValue == postalCode
                    join city in dbContext.Cities on zipCode.CityId equals city.Id
                    select city.CityName).FirstOrDefault().ToString();
        }

        public bool IsPostalCodeAvailable(string postalCode)
        {
            var result = (from user in dbContext.Users
                          where user.UserTypeId == 2
                          join userAdd in dbContext.UserAddresses on user.UserId equals userAdd.UserId
                          where userAdd.PostalCode == postalCode
                          select new { user.UserId }).Count();
            if (result > 0)
                return true;
            else 
                return false;
        }

        public IEnumerable<UserAddress> GetUserAddresses(int id)
        {
            return dbContext.UserAddresses.Where(add => add.UserId == id).ToList();
        }

        public int AddServiceRequest(ServiceRequest serviceRequest)
        {
            dbContext.ServiceRequests.Add(serviceRequest);
            dbContext.SaveChanges();
            int serviceId = serviceRequest.ServiceRequestId;
            return serviceId;
        }

        public UserAddress GetUserAddress(int addId)
        {
            return dbContext.UserAddresses.Find(addId);
        }

        public bool AddServiceReqAddress(ServiceRequestAddress serviceAddress)
        {
            dbContext.ServiceRequestAddresses.Add(serviceAddress);
            dbContext.SaveChanges();
            return true;
        }
    }
}
