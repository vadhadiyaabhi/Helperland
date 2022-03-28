using Helperland.IRepositories;
using Helperland.Models;
using Helperland.Repository;
using Helperland.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Implementations
{
    public class AdminImplementation : IAdminRepository
    {
        private readonly AppDbContext dbContext;

        public AdminImplementation(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<ServiceRequest>> GetAllServiceRequests()
        {
            return await dbContext.ServiceRequests.Include(s => s.User)
                                            .Include(s => s.ServiceProvider)
                                            .ThenInclude(sp => sp.RatingRatingToNavigations).AsSplitQuery()
                                            .Include(s => s.ServiceRequestAddresses)
                                            .ToListAsync();
        }

        public async Task<bool> EditService(EditServiceViewModel editService, int userId)
        {
            ServiceRequest service = await dbContext.ServiceRequests.FindAsync(editService.ServiceId);
            service.ServiceStartDate = Convert.ToDateTime(editService.Date + " " + editService.Time);
            service.ModifiedBy = userId;
            service.ModifiedDate = DateTime.Now;
            await dbContext.SaveChangesAsync();
            ServiceRequestAddress address = await dbContext.ServiceRequestAddresses.FindAsync(editService.AddressId);
            address.AddressLine1 = editService.AddressLine1;
            address.AddressLine2 = editService.AddressLine2;
            address.PostalCode = editService.ZipCode;
            address.City = editService.City;
            await dbContext.SaveChangesAsync();
            return true;
        }

        public IEnumerable<User> GetAllUsers()
        {
            //return dbContext.Users.Where(u => u.UserTypeId != 3).ToList();
            return dbContext.Users.ToList();
        }

        public async Task<bool> ApproveUnapproveUser(int userId, bool value, int adminId)
        {
            User user = await dbContext.Users.FindAsync(userId);
            user.IsApproved = !value;
            user.ModifiedBy = adminId;
            user.ModifiedDate = DateTime.Now;
            await dbContext.SaveChangesAsync();
            return user.IsApproved;
        }
    }
}
