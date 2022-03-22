using Helperland.Models;
using Helperland.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.IRepositories
{
    public interface IAdminRepository
    {
        public Task<IEnumerable<ServiceRequest>> GetAllServiceRequests();

        public Task<bool> EditService(EditServiceViewModel editService, int userId);

        public IEnumerable<User> GetAllUsers();
    }
}
