using Helperland.IRepositories;
using Helperland.Models;
using Helperland.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Helperland.Controllers
{
    public class UserController : Controller
    {
        private readonly IServiceRequestRepository servieviceRepository;
        private readonly IUserRepository userRepository;

        public UserController(IServiceRequestRepository servieviceRepository, IUserRepository userRepository)
        {
            this.servieviceRepository = servieviceRepository;
            this.userRepository = userRepository;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MyAccount()
        {
            return View();
        }

        public async Task<IActionResult> Mydetails()
        {
            User user = await userRepository.GetUser(5024);
            UserUpdateViewModel userViewModel = new UserUpdateViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Mobile = user.Mobile,
                DateOfBirth = user.DateOfBirth,
                LanguageId = user.LanguageId,

            };
            //Thread.Sleep(5000);
            return View(userViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUserDetails(UserUpdateViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                await userRepository.UpdateUser(userViewModel);
                return Json(new { userUpdateSuccess = true });
            }
            return Json(new { userUpdateFail = true });
        }

        public IActionResult UserAddresses()
        {
            var address = servieviceRepository.GetUserAddresses(5024);
            return View(address);
        }

        [HttpGet]
        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ResetPassword(ForgotPasswordViewModel passwordReset)
        {
            return View();
        }
    }
}
