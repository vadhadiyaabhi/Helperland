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

        public Task<User> UpdateUser(UserUpdateViewModel userViewModel);

        //public User GetUserByEmail(string email);
        public Task<User> GetUserByEmail(string email);
    }
}
