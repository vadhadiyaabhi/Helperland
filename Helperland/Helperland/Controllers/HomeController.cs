using Helperland.IRepositories;
using Helperland.Models;
using Helperland.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IContactRepository contactRepository;

        public IWebHostEnvironment HostingEnvironment;
        private readonly ILoginRepository login;

        public HomeController(ILogger<HomeController> logger,IContactRepository contact,
                              IWebHostEnvironment hostingEnvironment, ILoginRepository login)
        {
            _logger = logger;
            contactRepository = contact;
            HostingEnvironment = hostingEnvironment;
            this.login = login;
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
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactCreateViewModel contact)
        {
            ContactU result = null;
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
                result = contactRepository.Create(newContact);
                ViewBag.submitted = 1;
                ViewBag.Id = result.ContactUsId;
                ViewBag.name = result.Name;
                ModelState.Clear();
            }
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            
            return View("Index");
        }

        [HttpPost]
        public IActionResult Login([Bind("Email", "Password")] AuthenticationViewModel authenticationViewModel)
        {
            //Console.WriteLine("Login controller");
            if (ModelState.IsValid)
            {
                User user = login.Login(authenticationViewModel);
                if(user != null)
                {
                    if (user.IsActive)
                    {
                        if(user.UserTypeId == 1)
                        {
                            Console.WriteLine("User Login Successfull");
                            return RedirectToAction("User/Index");
                        }
                        else if(user.UserTypeId == 3)
                        {
                            return RedirectToAction("Admin/Index");
                        }
                        else if(user.UserTypeId == 2 && !user.IsApproved)
                        {
                            //Console.WriteLine("User not approved by admin");
                            return Json(new { notapproved = true});
                        }
                        else
                        {
                            Console.WriteLine("SP Login Successfull");
                            return RedirectToAction("SP/Index");
                        }
                    }
                    else
                    {
                        ModelState.Clear();
                        //Console.WriteLine("Not active");
                        return Json(new { notactive = true, email = user.Email, name = user.FirstName + " " + user.LastName, id=user.UserId});
                    }
                }
                //Console.WriteLine("Invalid credentials");
                return Json(new { invalid=true });
            }

            //Console.WriteLine("Fields are required");
            return Json(new {required = true});
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
