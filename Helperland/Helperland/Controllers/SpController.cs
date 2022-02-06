using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Controllers
{
    public class SpController : Controller
    {
        public IActionResult Index()
        {
            return View("service_provider");
        }
    }
}
