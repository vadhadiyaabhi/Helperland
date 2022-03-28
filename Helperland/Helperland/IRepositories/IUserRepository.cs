using Helperland.Models;
using Helperland.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.IRepositories
{
    public interface IUserRepository
    {
        public User Register(User user);

        public string GenerateToken(int id);

        public int AddToken(int id, string token);

        public Task<User> Activate(string token);

        public Task<bool> ResetForgotPassowrd(ForgotPasswordViewModel forgotPassword);

        public int GetCurrentTimeStamp();

        public Task<User> GetUser(int Id);

        public UserAddress GetSPAddress(int userId);

        public Task<User> UpdateUser(UserUpdateViewModel userViewModel, int userId);

        public Task<User> UpdateSp(SPUpdateViewModel spModel, int spId);

        //public User GetUserByEmail(string email);
        public Task<User> GetUserByEmail(string email);

        public Task<bool> DeleteAddress(int addressId);

        public Task<bool> ResetPassword(ResetPasswordViewModel resetPassword, int userId);

        public Task<bool> UpdateUserAddress(UserAddressViewModel userAddress);

        public Task<bool> UpdateSpAddress(SPUpdateViewModel userAddress);

        public IEnumerable<ServiceRequest> GetCurrentServices(int userId);

        public Task<int> DeleteServiceRequest(int ServiceReqId, int userId);

        public Task<ServiceRequest> GetServiceDetails(int ServiceId);

        public IEnumerable<ServiceRequest> GetUserServiceHistory(int userId);

        public Task<bool> AddRating(Rating rating);

        public Task<ServiceRequest> GetServiceWithUser(int ServiceId);

        public Task<ServiceRequest> GetService(int ServiceId);

        public ICollection<ServiceRequest> GetSPService(int spId);

        public Task<ServiceRequest> ReScheduleService(int ServiceId, DateTime dateTime);

        public ServiceTimeConflict ChechScheduleConflict(DateTime dateTime, decimal totalHours, int spId);

        public IEnumerable<ServiceRequest> GetNewServiceRequests(int spId);

        public Task<ServiceRequest> AcceptService(int ServiceId, int spId);

        public IEnumerable<ServiceRequest> GetSPServiceHistory(int spId);

        public IEnumerable<ServiceRequest> GetSPUpcomingServices(int spId);

        public Task<bool> MarkAsCompleted(int ServiceId, int spId);

        public Task<ServiceRequest> CancelServiceBySP(int serviceId, int spId);

        public IEnumerable<Rating> MyRatings(int spId);

        public IEnumerable<FavoriteAndBlocked> GetBlockedBySP(int spId);

        public IEnumerable<FavoriteAndBlocked> GetFavouritePros(int userId);

        public bool Blockunblock(int Id, bool Value);

        public bool FavUnfav(int Id, bool Value);

        public int GetNoOfCleanings(int userId, int spId);

        public IEnumerable<int> GetBlocked(int Id);

        public IEnumerable<int> GetSPBlockedByUser(int userId);

        public Task SendNewReqEmail(NewReqEmailModel newServiceEmail);

        public Task<bool> HasIssue(int serviceId);

        public IEnumerable<User> GetOtherSPs(int spId, string zipCode, bool hasPets);


        public IEnumerable<FavoriteAndBlocked> GetFavorites(int userId);
    }
}
