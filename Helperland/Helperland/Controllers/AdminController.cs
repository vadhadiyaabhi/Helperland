using Helperland.IRepositories;
using Helperland.Models;
using Helperland.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Helperland.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminRepository adminRepository;
        private readonly IUserRepository userRepository;
        private readonly IEmailService emailService;
        private readonly IServiceRequestRepository serviceRepository;

        public AdminController(IAdminRepository adminRepository, IUserRepository userRepository, IEmailService emailService,
                                IServiceRequestRepository serviceRepository)
        {
            this.adminRepository = adminRepository;
            this.userRepository = userRepository;
            this.emailService = emailService;
            this.serviceRepository = serviceRepository;
        }
        public async Task<IActionResult> Index()
        {
            var services = await adminRepository.GetAllServiceRequests();
            return View(services);
        }

        [Route("Admin/GetServiceDetails/{ServiceId}")]
        public async Task<IActionResult> GetServiceDetails(int ServiceId)
        {
            ServiceRequest service = await userRepository.GetServiceDetails(ServiceId);
            return View(service);
        }

        public async Task<IActionResult> CancelServiceRequest(int ServiceRequestId, string cancleMessage, string SPEmail, string UserEmail)
        {
            int adminId = Convert.ToInt32(HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);
            if (ServiceRequestId == 0 || string.IsNullOrEmpty(cancleMessage))
                return Json(false);
            Console.WriteLine(cancleMessage);
            int res = await userRepository.DeleteServiceRequest(ServiceRequestId, adminId);
            if (!string.IsNullOrEmpty(SPEmail) || !string.IsNullOrEmpty(UserEmail))
            {
                UserEmailOptions userEmailOptions = new UserEmailOptions
                {
                    ToEmails = new List<string>(),
                    templateName = "ServiceCancelledByAdmin",
                    Subject = "Service Cancelled By Admin",
                    Placeholder = new List<KeyValuePair<string, string>>()
                    {
                        //new KeyValuePair<string, string>("{{SPName}}", newServiceEmail.SPName),
                        new KeyValuePair<string, string>("{{Id}}", ServiceRequestId.ToString()),
                        new KeyValuePair<string, string>("{{ServiceCancelMessage}}", cancleMessage),
                    }
                };
                if (!string.IsNullOrEmpty(SPEmail))
                    userEmailOptions.ToEmails.Add(SPEmail);
                if (!string.IsNullOrEmpty(UserEmail))
                    userEmailOptions.ToEmails.Add(UserEmail);

                await emailService.SendTestEmail(userEmailOptions);
            }
            return Json(new { serviceDeleted = res, serviceId = ServiceRequestId });
        }

        [Route("Admin/EditAndReschedule/{ServiceId}")]
        public async Task<IActionResult> EditAndReschedule(int ServiceId)
        {
            ServiceRequest service = await userRepository.GetServiceDetails(ServiceId);
            EditServiceViewModel editService = new EditServiceViewModel()
            {
                Date = service.ServiceStartDate.ToString("dd-MM-yyyy"),
                Time = service.ServiceStartDate.ToString("HH:mm"),
                ServiceId = service.ServiceRequestId
            };
            foreach(ServiceRequestAddress address in service.ServiceRequestAddresses)
            {
                editService.AddressLine1 = address.AddressLine1;
                editService.AddressLine2 = address.AddressLine2;
                editService.ZipCode = address.PostalCode;
                editService.City = address.City;
                editService.AddressId = address.Id;
            }
            return View(editService);
        }

        [HttpPost]
        public async Task<IActionResult> EditAndReschedule(EditServiceViewModel editService)
        {
            if (ModelState.IsValid)
            {
                int adminId = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
                bool res = await adminRepository.EditService(editService, adminId);
            }
            return View();
        }

        public IActionResult UserManagement()
        {
            var users = adminRepository.GetAllUsers();
            return View(users);
        }

        public IActionResult SPAvailabilityInZipCode(string ZipCode)
        {
            bool spCount = serviceRepository.IsPostalCodeAvailable(ZipCode);
            if (spCount)
            {
                return Json(true);
            }
            else
                return Json($"No Service Provider available for ZipCode {ZipCode} as of now");
        }
    }
}
