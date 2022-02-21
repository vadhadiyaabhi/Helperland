using Helperland.IRepositories;
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
                    select new { city.CityName }).FirstOrDefault().ToString();
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
    }
}
