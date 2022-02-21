using Helperland.IRepositories;
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
        public IActionResult BookService(ServiceRequestViewModel serviceRequest)
        {
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
    }
}
