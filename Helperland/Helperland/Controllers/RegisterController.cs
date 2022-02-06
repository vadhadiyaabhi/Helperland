using Helperland.IRepositories;
using Helperland.Models;
using Helperland.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Controllers
{
    public class RegisterController : Controller
    {
        private IUserRepository UserRepository { get; }
        public IEmailService EmailService { get; }

        public RegisterController(IUserRepository userRepository, IEmailService emailService)
        {
            UserRepository = userRepository;
            EmailService = emailService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("Register");
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserViewModel user)
        {
            User result= null;
            if (ModelState.IsValid)
            {
                User newUser = new User
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Mobile = user.Mobile,
                    Password = user.Password,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    UserTypeId = 1,
                    IsActive = false,
                    IsRegisteredUser = true,
                    IsApproved = true
                };

                result = UserRepository.Register(newUser);
                string token = UserRepository.GenerateToken(result.UserId);
                UserRepository.AddToken(result.UserId, token);

                UserEmailOptions userEmailOptions = new UserEmailOptions
                {
                    ToEmails = new List<string>() { user.Email },
                    templateName = "EmailVerification",
                    Subject = "Email Verification",
                    Placeholder = new List<KeyValuePair<string, string>>()
                    {
                        new KeyValuePair<string, string>("{{UserName}}", user.FirstName + " " + user.LastName),
                        new KeyValuePair<string, string>("{{token}}", token),
                    }
                };

                await EmailService.SendTestEmail(userEmailOptions);


                //Clear ModelState only register successful, if email already exist then don't clear
                //ModelState.Clear();
            }
            return View("Register");
        }


        public IActionResult Activate(string token)
        {
            int result = UserRepository.Activate(token);
            if (!result)
            {
                //fail not activated
            }
            else
            {
                //success and make IsActive 1
            }

            return View();
        }
        
        [HttpGet]
        public IActionResult Sp_Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Sp_Register(UserViewModel user)
        {
            User result= null;
            if (ModelState.IsValid)
            {
                User newUser = new User
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Mobile = user.Mobile,
                    Password = user.Password,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    UserTypeId = 2,
                    IsActive = false,
                    IsRegisteredUser = true,
                    IsApproved = false
                };
                result = UserRepository.Register(newUser);
            }
            return View(result);
        }



    }
}
