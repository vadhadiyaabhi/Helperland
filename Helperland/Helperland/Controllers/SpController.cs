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

namespace Helperland.Controllers
{
    [Authorize(Roles = "2")]
    public class SPController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly IServiceRequestRepository serviceRepository;
        private readonly IServiceRequestRepository serviceRequest;
        private readonly IEmailService emailService;

        public SPController(IUserRepository userRepository, IServiceRequestRepository serviceRepository, 
                            IServiceRequestRepository serviceRequest, IEmailService emailService)
        {
            this.userRepository = userRepository;
            this.serviceRepository = serviceRepository;
            this.serviceRequest = serviceRequest;
            this.emailService = emailService;
        }


        public IActionResult Index()
        {
            int spId = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var upcomingServices = userRepository.GetSPUpcomingServices(spId);
            var newServiceRequests = userRepository.GetNewServiceRequests(spId);
            ViewBag.upcomingServices = upcomingServices.Count();
            ViewBag.newServiceRequests = newServiceRequests.Count();
            return View();
        }

        public IActionResult MyAccount()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Mydetails()
        {
            int spId = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            User user = await userRepository.GetUser(spId);
            UserAddress spAddress = userRepository.GetSPAddress(spId);
            SPUpdateViewModel spViewModel = new SPUpdateViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Mobile = user.Mobile,
                DateOfBirth = user.DateOfBirth,
                BirthDate = Convert.ToDateTime(user.DateOfBirth).Date.Day.ToString(),
                BirthYear = Convert.ToDateTime(user.DateOfBirth).Date.Year.ToString(),
                BirthMonth = Convert.ToDateTime(user.DateOfBirth).Date.Month.ToString(),
                LanguageId = user.LanguageId,
                NationalityId = user.NationalityId,
                AccountActive = user.IsApproved,
                Gender = user.Gender ?? 0,
                WorksWithPet = user.WorksWithPets
            };
            Console.WriteLine(spViewModel.Gender);
            if(spAddress != null)
            {
                spViewModel.AddressLine1 = spAddress.AddressLine1;
                spViewModel.AddressLine2 = spAddress.AddressLine2;
                spViewModel.ZipCode = spAddress.PostalCode;
                spViewModel.City = spAddress.City;
            }

            return View(spViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Mydetails(SPUpdateViewModel spViewModel)
        {
            //Console.WriteLine(userViewModel.DateOfBirth);
            if (ModelState.IsValid)
            {
                int userId = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
                await userRepository.UpdateSp(spViewModel,userId);
                if(spViewModel.AddressId == 0)
                {
                    UserAddress newSpAddress = new UserAddress()
                    {
                        AddressLine1 = spViewModel.AddressLine1,
                        AddressLine2 = spViewModel.AddressLine2,
                        City = spViewModel.City,
                        PostalCode = spViewModel.ZipCode,
                        Mobile = spViewModel.Mobile,
                        Email = HttpContext.User.FindFirst("Email").Value,
                        UserId = userId
                    };
                    serviceRepository.AddNewAddress(newSpAddress);
                }
                else
                {
                    await userRepository.UpdateSpAddress(spViewModel);
                }
                return Json(new { userUpdateSuccess = true });
            }
            //Console.WriteLine(userViewModel.BirthDate);
            return Json(new { userUpdateFail = true, view = Helper.RenderRazorViewToString(this, "Mydetails", spViewModel) });
        }


        //------------------Not in use now----------------------------------------------------------------
        public IActionResult UserAddress()
        {
            int spId = Convert.ToInt32(HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);
            UserAddress address = userRepository.GetSPAddress(spId);
            if(address != null)
            {
                UserAddressViewModel userAddressViewModel = new UserAddressViewModel()
                {
                    AddressLine1 = address.AddressLine1,
                    AddressLine2 = address.AddressLine2,
                    Mobile = address.Mobile,
                    City = address.City,
                    ZipCode = address.PostalCode,
                    AddressId = address.AddressId
                };
                return View(userAddressViewModel);
            }
            else
            {
                UserAddressViewModel userAddressViewModel = new UserAddressViewModel();
                return View(userAddressViewModel);
            }
        }


        //------------------Not in use now--------------------------------------------------------
        [HttpPost]
        public async Task<IActionResult> UpdateAddress(UserAddressViewModel userAddress)
        {
            if (ModelState.IsValid)
            {
                if (userAddress.AddressId == 0)
                {
                    UserAddress newAddress = new UserAddress()
                    {
                        AddressLine1 = userAddress.AddressLine1,
                        AddressLine2 = userAddress.AddressLine2,
                        City = userAddress.City,
                        Mobile = userAddress.Mobile,
                        PostalCode = userAddress.ZipCode,
                        UserId = Convert.ToInt32(HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value)
                    };

                    serviceRepository.AddNewAddress(newAddress);
                    return Json(new { addAddressSuccess = true });
                }
                else
                {
                    bool res = await userRepository.UpdateUserAddress(userAddress);
                    return Json(new { editAddressSuccess = true });
                }

            }
            return Json(new { addOrEditAddressFail = true, view = Helper.RenderRazorViewToString(this, "AddOrEditAddress", userAddress) });
        }
        //-------------------------------------------------------------------------------

        [HttpGet]
        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel passwordReset)
        {
            if (ModelState.IsValid)
            {
                int spId = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
                Console.WriteLine(spId);
                bool result = await userRepository.ResetPassword(passwordReset, spId);
                if (result)
                    return Json(new { passwordResetSuccess = true });
                else
                    return Json(new { passwordResetFail = true });
            }
            else
            {
                return Json(new { passwordResetValidateError = true, view = Helper.RenderRazorViewToString(this, "ResetPassword", passwordReset) });
            }

        }

        public IActionResult NewServiceRequests()
        {
            int spId = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var services = userRepository.GetNewServiceRequests(spId);
            return View(services);
        }

        [Route("SP/GetServiceDetails/{ServiceId}")]
        public async Task<IActionResult> GetServiceDetails(int ServiceId)
        {
            ServiceRequest service = await userRepository.GetServiceDetails(ServiceId);
            return View(service);
        }

        public async Task<IActionResult> AcceptService(int ServiceId)
        {
            int spId = Convert.ToInt32(HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);
            ServiceRequest service = await userRepository.GetService(ServiceId);
            Console.WriteLine(service.ServiceProviderId);
            if(service.ServiceProviderId != null)
            {
                return Json(new { serviceAlreadyAccepted = true, serviceId = ServiceId });
            }
            ServiceTimeConflict conflict = userRepository.ChechScheduleConflict(service.ServiceStartDate, service.SubTotal, spId);
            if (conflict.Conflict)
            {
                return Json(new { spServiceConflict = true, conflictServiceId=conflict.ServiceId, conflictStart = conflict.ConflictStartTime, conflictEnd = conflict.ConflictEndTime, conflictDate = conflict.ConflictDate });
            }
            ServiceRequest acceptedService = await userRepository.AcceptService(ServiceId, spId);
            var SPs = userRepository.GetOtherSPs(spId, service.ZipCode, service.HasPets);
            UserEmailOptions userEmailOptions = new UserEmailOptions
            {
                ToEmails = new List<string>(),
                templateName = "ServiceAccepted",
                Subject = "Service Request Is No More Available",
                Placeholder = new List<KeyValuePair<string, string>>()
                    {
                        new KeyValuePair<string, string>("{{ServiceId}}", Convert.ToString(service.ServiceRequestId)),
                    }
            };
            foreach (User sp in SPs)
            {
                userEmailOptions.ToEmails.Add(sp.Email);
                Console.WriteLine(sp.Email);
            }
            await emailService.SendTestEmail(userEmailOptions);
            return Json(new { serviceAccepted = true, serviceId = ServiceId, });
        }

        public IActionResult UpcomingServices()
        {
            int spId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var services = userRepository.GetSPUpcomingServices(spId);
            return View(services);
        }

        public async Task<IActionResult> CancleServiceRequestBySp(int ServiceRequestId, string cancleMessage, string UserEmail)
        {
            if (ServiceRequestId == 0 || string.IsNullOrEmpty(cancleMessage))
                return Json(false);
            int spId = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            ServiceRequest service = await userRepository.CancelServiceBySP(ServiceRequestId, spId);
            if(service.Status == 5)
            {
                IEnumerable<User> usersWithSameZipCode = serviceRequest.GetUserWithZipCode(service.ZipCode, service.HasPets);
                string ExtraServices = "";
                ExtraServices = ExtraServices + "<tr><td>Basic Service</td><td>" + service.ServiceHours + " Hrs </td><td>" + (service.ServiceHours * 18.00) + " €</td></tr>";
                service = await userRepository.GetServiceDetails(ServiceRequestId);
                foreach(ServiceRequestExtra extraService in service.ServiceRequestExtras)
                {
                    if(extraService.ServiceRequestExtraId == 1)
                        ExtraServices += "<tr><td>Inside cabinets</td><td>0.5 Hrs</td><td>9.00 €</td></tr>";
                    else if(extraService.ServiceRequestExtraId == 2)
                        ExtraServices += "<tr><td>Inside fridge</td><td>0.5 Hrs</td><td>9.00 €</td></tr>";
                    else if (extraService.ServiceRequestExtraId == 3)
                        ExtraServices += "<tr><td>Inside oven</td><td>0.5 Hrs</td><td>9.00 €</td></tr>";
                    else if (extraService.ServiceRequestExtraId == 4)
                        ExtraServices += "<tr><td>Laundry wash & dry</td><td>0.5 Hrs</td><td>9.00 €</td></tr>";
                    else if (extraService.ServiceRequestExtraId == 5)
                        ExtraServices += "<tr><td>Interio window</td><td>0.5 Hrs</td><td>9.00 €</td></tr>";
                }

                //-------Sending emails---------------------------
                NewReqEmailModel newReqEmail = new NewReqEmailModel
                {
                    DateTime = service.ServiceStartDate,
                    TotalAmount = service.TotalCost,
                    ExtraServices = ExtraServices,
                    UserName = service.User.FirstName + " " + service.User.LastName,
                    Emails = new List<string>(),
                    ServiceId = ServiceRequestId
                };
                foreach (User SP in usersWithSameZipCode)
                {
                    Console.WriteLine(SP.Email);
                    newReqEmail.Emails.Add(SP.Email);
                }
                await userRepository.SendNewReqEmail(newReqEmail);
            }
            return Json(new { serviceCancelledBySP = service.Status, serviceId = ServiceRequestId });
        }

        public async Task<IActionResult> CompleteServiceRequest(int ServiceId)
        {
            int spId = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            bool res =await userRepository.MarkAsCompleted(ServiceId, spId);
            if (res)
            {
                return Json(new { serviceCompleted = true, serviceId = ServiceId });
            }
            else
                return Json(false);
        }

        public IActionResult ServiceHistory()
        {
            int spId = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var services = userRepository.GetSPServiceHistory(spId);
            return View(services);
        }

        public IActionResult MyRatings()
        {
            int spId = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var ratings = userRepository.MyRatings(spId);
            return View(ratings);
        }

        public IActionResult BlockCustomer()
        {
            int spId = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var blocked = userRepository.GetBlockedBySP(spId);
            return View(blocked);
        }

        [Route("SP/Blockunblock/{ID}/{value}")]
        public string BlockUnblock(int Id, bool Value)
        {
            bool res = userRepository.Blockunblock(Id, Value);
            if (res)
                return "Unblock";
            else
                return "Block";
        }





    }
}
