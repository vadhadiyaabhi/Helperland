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
    public class UserController : Controller
    {
        private readonly IServiceRequestRepository serviceRequest;

        public UserController(IServiceRequestRepository serviceRequest)
        {
            this.serviceRequest = serviceRequest;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult BookService()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BookService(ServiceRequestViewModel newRequest)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine(newRequest.ExtraService1);
                int ES = 0;
                ES = newRequest.ExtraService1 ? ES + 1 : ES + 0;
                ES = newRequest.ExtraService2 ? ES + 1 : ES + 0;
                ES = newRequest.ExtraService3 ? ES + 1 : ES + 0;
                ES = newRequest.ExtraService4 ? ES + 1 : ES + 0;
                ES = newRequest.ExtraService5 ? ES + 1 : ES + 0;
                ServiceRequest service = new ServiceRequest
                {
                    UserId = 3021,
                    ServiceStartDate = Convert.ToDateTime(newRequest.Date + " " + newRequest.Time.ToString()),
                    ZipCode = newRequest.ZipCode,
                    Comments = newRequest.Comments,
                    HasPets = newRequest.HasPets,
                    PaymentDone = false,
                    CreatedDate = DateTime.Now,
                    ServiceHours = newRequest.ServiceHours,
                    ServiceHourlyRate = 18,
                    ExtraHours = ES * 0.5,
                    SubTotal = Convert.ToDecimal(newRequest.ServiceHours + ES),
                    TotalCost = Convert.ToDecimal((newRequest.ServiceHours + ES) * 18),
                };

                int serviceId = serviceRequest.AddServiceRequest(service);

                Console.WriteLine("service id is: "+serviceId);
                ViewBag.submitted = true;
                return Json(new { serviceAdded = true }) ;
            }
            
            return View();
            
        }

        public IActionResult IsPostalCodeAvailable(ZipCodeViewModel obj)
        {
            if(ModelState.IsValid)
            {
                var result = serviceRequest.IsPostalCodeAvailable(obj.ZipCode);

                if (result)
                {

                    return Json(new { zipCode = obj.ZipCode, zipAvailable = true });
                }

                else
                {
                    return Json(new { zipCode = obj.ZipCode, zipAvailable = false });
                }
            }
            return Json(new { zipInvalid = true });
                
        }

        public IActionResult GetUserAddress()
        {
            Console.WriteLine("Getting address");
            var address = serviceRequest.GetUserAddresses(3021);
            Console.WriteLine(address);
            return View(address);
        }

        [HttpGet]
        [Route("User/AddNewAddress/{zipCode}")]
        public IActionResult AddNewAddress(string zipCode)
        {
            string city = serviceRequest.GetCityName(zipCode);
            UserAddressViewModel userAddress = new UserAddressViewModel
            {
                City = city,
                ZipCode = zipCode
            };
            return View(userAddress);
        }
    }
}
