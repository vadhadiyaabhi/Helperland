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

        public HomeController(ILogger<HomeController> logger,IContactRepository contact,
                              IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;
            contactRepository = contact;
            HostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
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

        public IActionResult Login()
        {
            ViewBag.Login = "true";
            return View("Index");
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
