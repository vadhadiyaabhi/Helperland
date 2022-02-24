using BC = BCrypt.Net.BCrypt;
using Helperland.IRepositories;
using Helperland.Models;
using Helperland.Repository;
using Helperland.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Implementations
{
    public class LoginImplementation : ILoginRepository
    {
        private readonly AppDbContext DbContext;

        public LoginImplementation(AppDbContext DbContext)
        {
            this.DbContext = DbContext;
        }
        public  User Login(AuthenticationViewModel authenticationViewModel)
        {
            User user = DbContext.Users.Where(u => u.Email == authenticationViewModel.Email).FirstOrDefault();
            if (user != null)
            {
                if (BC.Verify(authenticationViewModel.Password, user.Password))
                {
                    return user;
                }
                else
                    return null;
            }
            //User user = DbContext.Users.Where(u => u.Email == authenticationViewModel.Email && u.Password == authenticationViewModel.Password).FirstOrDefault();
            return user;
        }
    }
}
