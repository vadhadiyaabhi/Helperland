using Helperland.IRepositories;
using Helperland.Models;
using Helperland.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Helperland.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IContactRepository contactRepository;
        public IWebHostEnvironment HostingEnvironment;
        private readonly ILoginRepository login;
        private readonly IUserRepository userRepository;

        public HomeController(ILogger<HomeController> logger,IContactRepository contact,
                              IWebHostEnvironment hostingEnvironment, ILoginRepository login, IUserRepository userRepository)
        {
            _logger = logger;
            contactRepository = contact;
            HostingEnvironment = hostingEnvironment;
            this.login = login;
            this.userRepository = userRepository;
            //Console.WriteLine("Checking controller execution");
        }

        public IActionResult Index()
        {
            ViewBag.Login = 1;
            return View();
        }
        public IActionResult Prices()
        {
            return View();
        }
        public IActionResult FAQs()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }

        [HttpGet]
        [Route("Home/Contact")]
        [Route("Home/Contact/{serviceId}")]
        public IActionResult Contact(int? serviceId = 0)
        {
            //Console.WriteLine("Report service id is " + serviceId);
            ViewBag.ServiceId = serviceId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Contact(ContactCreateViewModel contact)
        {
            ContactU result = null;
            Console.WriteLine("Checking form submitting without validation or not");
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if(contact.Photo != null)
                {
                    string uploadFolder = Path.Combine(HostingEnvironment.WebRootPath, "images/contactFile");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + contact.Photo.FileName;
                    string FilePath = Path.Combine(uploadFolder, uniqueFileName);
                    contact.Photo.CopyTo(new FileStream(FilePath, FileMode.Create));

                }

                ContactU newContact = new ContactU
                {
                    Name = contact.firstname + " " + contact.lastname,
                    PhoneNumber = contact.PhoneNumber,
                    Email = contact.Email,
                    Subject = contact.Subject,
                    Message = contact.Message,
                    UploadFileName = uniqueFileName,
                    CreatedOn = DateTime.Now
                };
                if(contact.ServiceId != 0)
                {
                    newContact.CreatedBy = Convert.ToInt32(HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);
                }
                result = contactRepository.Create(newContact);
                if (contact.ServiceId != 0)
                {
                    bool hasIssue = await userRepository.HasIssue(contact.ServiceId);
                }
                ViewBag.submitted = 1;
                ViewBag.Id = result.ContactUsId;
                ViewBag.name = result.Name;
                ModelState.Clear();
            }
            return View();
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = "/")
        {
            AuthenticationViewModel authenticationViewModel = new AuthenticationViewModel();
            authenticationViewModel.ReturnUrl = returnUrl;

            //NOTEc: Without this function, ReturnUrl is not working, PLEASE FIX it first

            authenticationViewModel.LoginModal = true;
            return View("Index", authenticationViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login([Bind("Email", "Password", "ReturnUrl", "RememberMe")] AuthenticationViewModel authenticationViewModel)
        {
            Console.WriteLine(authenticationViewModel.ReturnUrl);
            if (ModelState.IsValid)
            {
                User user = login.Login(authenticationViewModel);
                if(user != null)
                {

                    if (!user.IsActive)
                    {
                        ModelState.Clear();
                        //Console.WriteLine("Not active");
                        return Json(new { notactive = true, email = user.Email, name = user.FirstName + " " + user.LastName, id = user.UserId });
                    }
                    if (user.UserTypeId == 2 && !user.IsApproved)
                    {
                        return Json(new { notapproved = true });
                    }
                    else
                    {
                        var claims = new List<Claim>()
                            {
                                new Claim(ClaimTypes.NameIdentifier, Convert.ToString(user.UserId)),
                                new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                                new Claim(ClaimTypes.Role, Convert.ToString(user.UserTypeId)),
                                new Claim("Email", user.Email),
                                new Claim("FirstName", user.FirstName),
                                new Claim("Helperland ", "AuthCookie")
                            };

                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                            new AuthenticationProperties()
                            {
                                IsPersistent = authenticationViewModel.RememberMe
                            });

                        if (authenticationViewModel.ReturnUrl == "/")
                        {
                            if (user.UserTypeId == 1) authenticationViewModel.ReturnUrl = "/User";
                            if (user.UserTypeId == 2) authenticationViewModel.ReturnUrl = "/SP";
                            if (user.UserTypeId == 3) authenticationViewModel.ReturnUrl = "/Admin";
                        }
                        return Json(new { loginSuccess = true, returnUrl = authenticationViewModel.ReturnUrl });
                    }
                    
                }
                //Console.WriteLine("Invalid credentials");
                return Json(new { invalid=true });
            }

            //Console.WriteLine("Fields are required");
            return Json(new {required = true});
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect("/Home/Login");
        }
        public IActionResult Register()
        {
            return View("../Register/Register");
        }

        public IActionResult Sp_Register()
        {
            return View("../Register/Sp_Register");
        }

        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
