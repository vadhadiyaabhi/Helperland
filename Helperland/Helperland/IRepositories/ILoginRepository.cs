using Helperland.Models;
using Helperland.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.IRepositories
{
    public interface ILoginRepository
    {
        public User Login(AuthenticationViewModel authenticationViewModel);
    }
}
