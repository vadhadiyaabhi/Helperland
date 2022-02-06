using Helperland.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Implementations
{
    public interface IAuthenticationRepository
    {
        public string Authenticate(AuthenticationViewModel model);
    }
}
