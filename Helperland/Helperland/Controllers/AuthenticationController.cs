using Helperland.Implementations;
using Helperland.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Controllers
{
    public class AuthenticationController : Controller
    {
        public AuthenticationController(IAuthenticationRepository authentication)
        {
            Authentication = authentication;
        }

        public IAuthenticationRepository Authentication { get; }

        [HttpPost]
        public IActionResult Index(AuthenticationViewModel model)
        {
            if (ModelState.IsValid)
            {
                string token= Authentication.Authenticate(model);
            }
            return View();
        }
    }
}
