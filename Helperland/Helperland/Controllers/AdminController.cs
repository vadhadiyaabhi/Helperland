using Helperland.IRepositories;
using Helperland.Models;
using Helperland.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static Helperland.Helper;

namespace Helperland.Controllers
{
    [Authorize(Roles = "3")]
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
        public async Task<IActionResult> Index(int serviceId=0, string Customer=null, string SP=null, int Status = 0, string from_date = null, string to_date =null)
        {
            var services = await adminRepository.GetAllServiceRequests();
            if(serviceId != 0)
            {
                services = services.Where(x => x.ServiceRequestId == serviceId).ToList();
            }
            if (!string.IsNullOrEmpty(Customer))
            {
                services = services.Where(x => (x.User.FirstName + " " + x.User.LastName).Contains(Customer)).ToList();
            }
            if (!string.IsNullOrEmpty(SP))
            {
                services = services.Where(x => (x.ServiceProvider.FirstName + " " + x.ServiceProvider.LastName).Contains(Customer)).ToList();
            }
            if (Status != 0)
            {
                services = services.Where(x => x.Status == Status).ToList();
            }
            DateTime From_Date;
            DateTime To_Date;
            if (!string.IsNullOrEmpty(from_date))
            {
                From_Date = Convert.ToDateTime(from_date);
                services = services.Where(x => x.ServiceStartDate <= From_Date).ToList();
            }
            if (!string.IsNullOrEmpty(to_date))
            {
                To_Date = Convert.ToDateTime(to_date);
                services = services.Where(x => x.ServiceStartDate >= To_Date).ToList();
            }
            ViewBag.Customer = Customer;
            ViewBag.SP = SP;
            ViewBag.serviceId = serviceId;
            ViewBag.from_date = from_date;
            ViewBag.to_date = to_date;
            ViewBag.Status = Status;
            

            return View(services);
        }

        [Route("Admin/GetServiceDetails/{ServiceId}")]
        [NoDirectAccess]
        public async Task<IActionResult> GetServiceDetails(int ServiceId)
        {
            ServiceRequest service = await userRepository.GetServiceDetails(ServiceId);
            return View(service);
        }

        [NoDirectAccess]
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

        [HttpGet]
        [Route("Admin/EditAndReschedule/{ServiceId}")]
        [NoDirectAccess]
        public async Task<IActionResult> EditAndReschedule(int ServiceId)
        {
            ServiceRequest service = await userRepository.GetServiceDetails(ServiceId);
            EditServiceViewModel editService = new EditServiceViewModel()
            {
                Date = service.ServiceStartDate.ToString("yyyy-MM-dd"),
                Time = service.ServiceStartDate.ToString("HH:mm"),
                ServiceId = service.ServiceRequestId,
                UserEmail = service.User.Email,
                SPEmail = (service.ServiceProvider != null ? service.ServiceProvider.Email : ""),
                UserId = service.UserId,
                SPId = service.ServiceProviderId ?? 0,
                SubTotal = service.SubTotal
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
        [NoDirectAccess]
        public async Task<IActionResult> EditAndReschedule(EditServiceViewModel editService)
        {
            if (ModelState.IsValid)
            {
                int adminId = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
                DateTime dateTime = Convert.ToDateTime(editService.Date + " " + editService.Time);
                if (editService.SPId != 0)
                {
                    ServiceTimeConflict conflict = userRepository.ChechScheduleConflict(dateTime, editService.SubTotal, Convert.ToInt32(editService.SPId));

                    Console.WriteLine(conflict.Conflict);
                    if (conflict.Conflict)
                    {
                        return Json(new { reScheduleFail = true, conflictStart = conflict.ConflictStartTime, conflictEnd = conflict.ConflictEndTime, conflictDate = conflict.ConflictDate });
                    }

                }
                bool res = await adminRepository.EditService(editService, adminId);
                if (res)
                {
                    if (!string.IsNullOrEmpty(editService.SPEmail) || !string.IsNullOrEmpty(editService.UserEmail))
                    {
                        UserEmailOptions userEmailOptions = new UserEmailOptions
                        {
                            ToEmails = new List<string>(),
                            templateName = "ServiceUpdatedByAdmin",
                            Subject = "Service Updated By Admin",
                            Placeholder = new List<KeyValuePair<string, string>>()
                            {
                                new KeyValuePair<string, string>("{{Id}}", editService.ServiceId.ToString()),
                                new KeyValuePair<string, string>("{{Date}}", dateTime.ToString("dd-MM-yyyy").Replace('-','/')),
                                new KeyValuePair<string, string>("{{Time}}", dateTime.ToString("HH:mm") + " to " + dateTime.AddHours(Convert.ToDouble(editService.SubTotal)).ToString("HH:mm")),
                                new KeyValuePair<string, string>("{{AddressLine1}}", editService.AddressLine1),
                                new KeyValuePair<string, string>("{{AddressLine2}}", editService.AddressLine2),
                                new KeyValuePair<string, string>("{{ZipCode}}", editService.ZipCode),
                                new KeyValuePair<string, string>("{{City}}", editService.City),
                                new KeyValuePair<string, string>("{{ServiceUpdateMessage}}", editService.Comment),
                            }
                        };
                        if (!string.IsNullOrEmpty(editService.SPEmail))
                            userEmailOptions.ToEmails.Add(editService.SPEmail);
                        if (!string.IsNullOrEmpty(editService.UserEmail))
                            userEmailOptions.ToEmails.Add(editService.UserEmail);

                        await emailService.SendTestEmail(userEmailOptions);
                    }
                    return Json(new { reScheduleSuccess = true, serviceId = editService.ServiceId, reScheduleDate = dateTime.ToString("dd-MM-yyyy").Replace('-', '/'), reScheduleTime = dateTime.ToString("HH:mm") + " to " + dateTime.AddHours(Convert.ToDouble(editService.SubTotal)).ToString("HH:mm") });
                }
                else
                    return Json(false);
            }
            return Json(new { editServiceFail = true, view = Helper.RenderRazorViewToString(this, "EditAndReschedule", editService) });
        }

        public IActionResult UserManagement(string Name = null, int UserRole = 0, string Mobile = null, string ZipCode = null)
        {
            var users = adminRepository.GetAllUsers();
            if (!string.IsNullOrEmpty(Name))
            {
                users = users.Where(x => (x.FirstName + " " + x.LastName).Contains(Name)).ToList();
            }
            if(UserRole != 0)
            {
                users = users.Where(x => x.UserTypeId == UserRole).ToList();
            }
            if(!string.IsNullOrEmpty(Mobile))
            {
                users = users.Where(x => x.Mobile == Mobile).ToList();
            }
            if(!string.IsNullOrEmpty(ZipCode))
            {
                users = users.Where(x => x.ZipCode == ZipCode).ToList();
            }
            ViewBag.Name = Name;
            ViewBag.UserRole = UserRole;
            ViewBag.Mobile = Mobile;
            ViewBag.ZipCode = ZipCode;
            return View(users);
        }

        [Route("Admin/ApproveUser/{userId}/{isApprove}")]
        [NoDirectAccess]
        public async Task<string> ApproveUser(int userId, bool isApprove)
        {
            int adminId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            bool res = await adminRepository.ApproveUnapproveUser(userId, isApprove, adminId);
            if (res)
                return "InActive";
            else
                return "Active";
        }

        [NoDirectAccess]
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

        [Route("Admin/FetchUserNames/{name}")]
        [NoDirectAccess]
        public IActionResult FetchUserNames(string name)
        {
            var users = adminRepository.GetAllUsers();
            List<string> names = users.Where(x => x.FirstName.Contains(name) || x.LastName.Contains(name)).Select(x => x.FirstName + " " + x.LastName).Distinct().ToList();
            return Json(names);
        }

        [Route("Admin/FetchCustomerNames/{name}")]
        [NoDirectAccess]
        public IActionResult FetchCustomerNames(string name)
        {
            var users = adminRepository.GetAllUsers();
            List<string> names = users.Where(x => (x.FirstName.Contains(name) || x.LastName.Contains(name)) && x.UserTypeId == 1).Select(x => x.FirstName + " " + x.LastName).Distinct().ToList();
            return Json(names);
        }

        [Route("Admin/FetchSPNames/{name}")]
        [NoDirectAccess]
        public IActionResult FetchSPNames(string name)
        {
            var users = adminRepository.GetAllUsers();
            List<string> names = users.Where(x => (x.FirstName.Contains(name) || x.LastName.Contains(name)) && x.UserTypeId == 2).Select(x => x.FirstName + " " + x.LastName).Distinct().ToList();
            return Json(names);
        }
    }
}
