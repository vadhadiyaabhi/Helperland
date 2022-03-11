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

        public Task<int> DeleteServiceRequest(int ServiceReqId);
    }
}
