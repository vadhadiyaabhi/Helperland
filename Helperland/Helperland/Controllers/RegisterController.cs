using BC = BCrypt.Net.BCrypt;
using Helperland.IRepositories;
using Helperland.Models;
using Helperland.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Controllers
{
    public class RegisterController : Controller
    {
        private readonly ICipherService cipherService;

        private IUserRepository UserRepository { get; }
        public IEmailService EmailService { get; }

        public RegisterController(IUserRepository userRepository, IEmailService emailService,
                                  ICipherService cipherService)
        {
            UserRepository = userRepository;
            EmailService = emailService;
            this.cipherService = cipherService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("Register");
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var user =await UserRepository.GetUserByEmail(email);
            Console.WriteLine(HttpContext.Request.Path);
            if (user == null)
            {
                return Json(true);
            }

            else
            {
                return Json($"Email {email} is already in use");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Index(UserViewModel user)
        {
            User result= null;
            Console.WriteLine("Backend call before front end validation");
            if (ModelState.IsValid)
            {
                result =await UserRepository.GetUserByEmail(user.Email);
                if (result != null)
                {
                    ViewBag.userExist = true;
                    if(user.UserTypeId == 1)
                    {
                        return View("Register");
                    }
                    else
                    {
                        return View("Sp_Register");
                    }
                }

                User newUser = new User
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Mobile = user.Mobile,
                    Password = BC.HashPassword(user.Password),
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    UserTypeId = user.UserTypeId,
                    IsActive = false,
                    IsRegisteredUser = true,
                    
                };
                if(user.UserTypeId == 1)
                {
                    newUser.IsApproved = true;
                }
                else
                {
                    newUser.IsApproved = false;
                }

                result = UserRepository.Register(newUser);
                //Clear ModelState only register successful, if email already exist then don't clear
                ModelState.Clear();

                EmailConfirm emailConfirm = new EmailConfirm()
                {
                    Id = result.UserId,
                    Email = result.Email,
                    name = result.FirstName + " " + result.LastName
                };
                await SendConfirmEmail(emailConfirm);
                emailConfirm.EmailSent = true;
                return View("email-confirm", emailConfirm);
            }
            if (user.UserTypeId == 1)
            {
                return View("Register");
            }
            else
            {
                return View("Sp_Register");
            }
            
        }

        public async Task SendConfirmEmail(EmailConfirm emailConfirm)
        {
            string token = UserRepository.GenerateToken(emailConfirm.Id);
            //token = cipherService.Encrypt(token);
            UserRepository.AddToken(emailConfirm.Id, token);

            UserEmailOptions userEmailOptions = new UserEmailOptions
            {
                ToEmails = new List<string>() { emailConfirm.Email },
                templateName = "EmailVerification",
                Subject = "Email Verification",
                Placeholder = new List<KeyValuePair<string, string>>()
                    {
                        new KeyValuePair<string, string>("{{UserName}}", emailConfirm.name),
                        new KeyValuePair<string, string>("{{token}}", token),
                    }
            };

            await EmailService.SendTestEmail(userEmailOptions);
        }

        public async Task SendForgetPasswordEmail(int Id, string Email, string Name)
        {
            string token = UserRepository.GenerateToken(Id);
            UserRepository.AddToken(Id, token);

            UserEmailOptions userEmailOptions = new UserEmailOptions()
            {
                ToEmails = new List<string>() { Email },
                templateName = "ForgotPassword",
                Subject = "Reset Password",
                Placeholder = new List<KeyValuePair<String, String>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}", Name),
                    new KeyValuePair<string, string>("{{token}}", token),
                }
            };

            await EmailService.SendTestEmail(userEmailOptions);
        }

        [Route("Register/Activate/{token}")]
        public async Task<IActionResult> Activate(string token)
        {
            //token = cipherService.Decrypt(token);
            User result =await UserRepository.Activate(token);
            if (result != null)
            {
                if (result.IsActive == true)
                {
                    Console.WriteLine("inside if");
                    EmailConfirm confirm = new EmailConfirm()
                    {
                        EmailConfirmed = true
                    };
                    return View("email-confirm", confirm);
                }
                else
                {
                    EmailConfirm confirm = new EmailConfirm()
                    {
                        EmailConfirmed = false,
                        Invalid = true,
                        Email = result.Email
                    };
                    return View("email-confirm", confirm);
                }
            }
            else
            {
                EmailConfirm confirm = new EmailConfirm()
                {
                    EmailConfirmed = false,
                    userNotExist = true
                };
                return View("email-confirm", confirm);
            }
            //return View("Register");
        }

        [Route("Register/ForgotPassword/{token}")]
        public IActionResult ForgotPassword(string token)
        {
            ForgotPasswordViewModel forgotPasswordViewModel = new ForgotPasswordViewModel()
            {
                Token = token
            };
            return View(forgotPasswordViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPasswordEmail(string email)
        {
            if (!String.IsNullOrEmpty(email))
            {
                User user = await UserRepository.GetUserByEmail(email);
                if(user != null)
                {
                    await SendForgetPasswordEmail(user.UserId, user.Email, user.FirstName + " " + user.LastName);
                    return Json(new { mailsent = true });
                }
                else
                {
                    return Json(new { notexist=true });
                }
            }
            return Json(new { required = true});

        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ForgotPasswordViewModel forgotPassword)
        {
            if (ModelState.IsValid)
            {
                bool res =await UserRepository.ResetForgotPassowrd(forgotPassword);
                if (res)
                {
                    return Json(new { reset = true});
                }

                return Json(new { reseterror = true });
            }
            Console.WriteLine(forgotPassword.Token);
            return View("ForgotPassword");
        }
        
        [HttpGet]
        public IActionResult Sp_Register()
        {
            return View();
        }

        [HttpPost("Register/ResendConfirmEmail")]
        public async Task<IActionResult> ResendConfirmEmail(EmailConfirm emailConfirm)
        {
            User user =await UserRepository.GetUserByEmail(emailConfirm.Email);
            if (user != null)
            {
                if (user.IsActive != true)
                {
                    EmailConfirm confirm = new EmailConfirm()
                    {
                        Id = user.UserId,
                        Email = user.Email,
                        name = user.FirstName + " " + user.LastName
                    };

                    await SendConfirmEmail(confirm);
                    emailConfirm.EmailSent = true;
                    return View("email-confirm", emailConfirm);
                }
                emailConfirm.EmailConfirmed = true;
                return View("email-confirm", emailConfirm);
            }
            return NotFound();
        }



    }
}
