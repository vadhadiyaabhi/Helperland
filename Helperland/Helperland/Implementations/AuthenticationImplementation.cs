using Helperland.Repository;
using Helperland.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Implementations
{
    public class AuthenticationImplementation : IAuthenticationRepository
    {
        public AuthenticationImplementation(AppDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public AppDbContext DbContext { get; }

        public string Authenticate(AuthenticationViewModel model)
        {
            return "yes";
        }
    }
}
