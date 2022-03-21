using Helperland.IRepositories;
using Helperland.Models;
using Helperland.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Helperland.Controllers
{
    [Authorize(Roles = "1")]
    public class UserController : Controller
    {
        private readonly IServiceRequestRepository serviceRepository;
        private readonly IUserRepository userRepository;
        private readonly IEmailService emailService;

        public UserController(IServiceRequestRepository serviceRepository, IUserRepository userRepository, IEmailService emailService)
        {
            this.serviceRepository = serviceRepository;
            this.userRepository = userRepository;
            this.emailService = emailService;
        }

        public IActionResult Index()
        {
            int userId = Convert.ToInt32(HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);
            var services = userRepository.GetCurrentServices(userId);
            return View(services);
        }

        public IActionResult MyAccount()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Mydetails()
        {
            int userId = Convert.ToInt32(HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);
            User user = await userRepository.GetUser(userId);
            UserUpdateViewModel userViewModel = new UserUpdateViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Mobile = user.Mobile,
                DateOfBirth = user.DateOfBirth,
                LanguageId = user.LanguageId
            };

            if(user.DateOfBirth != null)
            {
                userViewModel.BirthDate = Convert.ToDateTime(user.DateOfBirth).Date.Day.ToString();
                userViewModel.BirthYear = Convert.ToDateTime(user.DateOfBirth).Date.Year.ToString();
                userViewModel.BirthMonth = Convert.ToDateTime(user.DateOfBirth).Date.Month.ToString();
            }


            //Console.WriteLine(Convert.ToDateTime(user.DateOfBirth).TimeOfDay);
            //Console.WriteLine((Convert.ToString(Convert.ToDateTime(service.ServiceStartDate).TimeOfDay).Substring(0,5));     // To get time hh:mm formate
            //Console.WriteLine((service.ServiceStartDate.AddHours(Convert.ToDouble(service.SubTotal))));     // To Add time in datetime use Add method, AddHours, AddMinutes, AddSecond etc...
            //String.Format("{0:yyyy/MM/dd}", service.ServiceStartDate)
            //Thread.Sleep(5000);
            return View(userViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Mydetails(UserUpdateViewModel userViewModel)
        {
            //Console.WriteLine(userViewModel.DateOfBirth);
            if (ModelState.IsValid)
            {
                int userId = Convert.ToInt32(HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);
                await userRepository.UpdateUser(userViewModel, userId);
                return Json(new { userUpdateSuccess = true });
            }
            //Console.WriteLine(userViewModel.BirthDate);
            return Json(new { userUpdateFail = true, view = Helper.RenderRazorViewToString(this, "Mydetails", userViewModel) });
        }

        public IActionResult UserAddresses()
        {
            int userId = Convert.ToInt32(HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);
            var address = serviceRepository.GetUserAddresses(userId);
            return View(address);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAddress(string addressId)
        {
            bool result = await userRepository.DeleteAddress(Convert.ToInt32(addressId));
            if(result)
                return Json(new { addressDeleted = true });
            else
                return Json(new { addressNotDeleted = true });
        }

        [HttpGet]
        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel passwordReset)
        {
            Console.WriteLine("password before valid state");
            if (ModelState.IsValid)
            {
                Console.WriteLine("password after valid state");
                int userId = Convert.ToInt32(HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);
                Console.WriteLine(userId);
                bool result = await userRepository.ResetPassword(passwordReset, userId);
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

        [HttpGet]
        [Route("User/AddOrEditAddress/{addressId}")]
        public IActionResult AddOrEditAddress(int addressId)
        {
            if(addressId == 0)
            {
                UserAddressViewModel userAddressViewModel = new UserAddressViewModel();
                return View(userAddressViewModel);
            }
            else
            {
                UserAddress userAddress = serviceRepository.GetUserAddress(addressId);
                UserAddressViewModel userAddressViewModel = new UserAddressViewModel()
                {
                    AddressLine1 = userAddress.AddressLine1,
                    AddressLine2 = userAddress.AddressLine2,
                    ZipCode = userAddress.PostalCode,
                    Mobile = userAddress.Mobile,
                    City = userAddress.City,
                    AddressId = userAddress.AddressId
                };

                return View(userAddressViewModel);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddOrEditAddress(UserAddressViewModel userAddress)
        {
            if (ModelState.IsValid)
            {
                if(userAddress.AddressId == 0)
                {
                    UserAddress newAddress = new UserAddress()
                    {
                        AddressLine1 = userAddress.AddressLine1,
                        AddressLine2 = userAddress.AddressLine2,
                        City = userAddress.City,
                        Mobile = userAddress.Mobile,
                        PostalCode = userAddress.ZipCode,
                        Email = HttpContext.User.FindFirst("Email").Value,
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

        public async Task<IActionResult> DeleteServiceRequest(int ServiceRequestId, string cancleMessage, string SPEmail)
        {
            if (ServiceRequestId == 0 || string.IsNullOrEmpty(cancleMessage))
                return Json(false);
            Console.WriteLine(cancleMessage);
            int res = await userRepository.DeleteServiceRequest(ServiceRequestId);
            if (!string.IsNullOrEmpty(SPEmail))
            {
                UserEmailOptions userEmailOptions = new UserEmailOptions
                {
                    ToEmails = new List<string>() { SPEmail },
                    templateName = "ServiceCancelled",
                    Subject = "Service Request Cancelled",
                    Placeholder = new List<KeyValuePair<string, string>>()
                    {
                        //new KeyValuePair<string, string>("{{SPName}}", newServiceEmail.SPName),
                        new KeyValuePair<string, string>("{{Id}}", ServiceRequestId.ToString()),
                        new KeyValuePair<string, string>("{{ServiceCancelMessage}}", cancleMessage),
                    }
                };

                await emailService.SendTestEmail(userEmailOptions);
            }
            return Json(new { serviceDeleted = res, serviceId = ServiceRequestId });
        }

        [Route("User/GetServiceDetails/{ServiceId}")]
        public async Task<IActionResult> GetServiceDetails(int ServiceId)
        {
            ServiceRequest service = await userRepository.GetServiceDetails(ServiceId);
            return View(service);
        }

        public IActionResult ServiceHistory()
        {
            int userId = Convert.ToInt32(HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);
            var history = userRepository.GetUserServiceHistory(userId);
            return View(history);
        }

        public async Task<IActionResult> RateSP([Bind("ServiceId", "SPId", "OnTimeArrival", "Friendly", "QualityOfService", "Comment")] RatingViewModel ratingViewModel)
        {
            int userId = Convert.ToInt32(HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);
            //Console.WriteLine(ratingViewModel.OnTimeArrival);
            //Console.WriteLine(ratingViewModel.Friendly);
            //Console.WriteLine(ratingViewModel.QualityOfService);
            Rating rating = new Rating()
            {
                ServiceRequestId = ratingViewModel.ServiceId,
                RatingDate = ratingViewModel.RatingDate,
                RatingFrom = userId,
                RatingTo = ratingViewModel.SPId,
                Comments = ratingViewModel.Comment,
                OnTimeArrival = ratingViewModel.OnTimeArrival,
                Friendly = ratingViewModel.Friendly,
                QualityOfService = ratingViewModel.QualityOfService,
                Ratings = (ratingViewModel.QualityOfService + ratingViewModel.Friendly + ratingViewModel.OnTimeArrival) / 3 
            };
            Console.WriteLine("Ratings is " + rating.Ratings);

            bool res = await userRepository.AddRating(rating);
            if (res)
                return Json(new { ratingSuccess = true });
            else
                return Json(false);
        }

        public async Task<IActionResult> ReScheduleService(int ServiceId, string Date, string Time)
        {
            if(ServiceId != 0 && !string.IsNullOrEmpty(Date) && !string.IsNullOrEmpty(Time))
            {
                ServiceRequest service = await userRepository.GetService(ServiceId);
                DateTime dateTime = Convert.ToDateTime(Date + " " + Time);
                if(service.ServiceProviderId != null)
                {
                    ServiceTimeConflict conflict = userRepository.ChechScheduleConflict(dateTime,service.SubTotal,Convert.ToInt32(service.ServiceProviderId));

                    Console.WriteLine(conflict.Conflict);
                    if (conflict.Conflict)
                    {
                        return Json(new { reScheduleFail = true, conflictStart = conflict.ConflictStartTime, conflictEnd = conflict.ConflictEndTime, conflictDate = conflict.ConflictDate });
                    }
                    
                }
                ServiceRequest reScheduledservice = await userRepository.ReScheduleService(ServiceId, dateTime);
                service = await userRepository.GetService(ServiceId);

                //First Check whether service has SP assigned or not ----------------------------IMP
                if (service.ServiceProvider != null && service.ServiceProviderId != null)
                {
                    Console.WriteLine(service.ServiceProvider.Email);
                    UserEmailOptions userEmailOptions = new UserEmailOptions
                    {
                        ToEmails = new List<string>() { service.ServiceProvider.Email },
                        templateName = "ServiceReScheduled",
                        Subject = "Service Request ReScheduled",
                        Placeholder = new List<KeyValuePair<string, string>>()
                        {
                            new KeyValuePair<string, string>("{{Name}}", service.ServiceProvider.FirstName + " " + service.ServiceProvider.LastName),
                            new KeyValuePair<string, string>("{{ServiceId}}", service.ServiceRequestId.ToString()),
                            new KeyValuePair<string, string>("{{Date}}", service.ServiceStartDate.ToString("dd-MM-yyyy").Replace('-', '/')),
                            new KeyValuePair<string, string>("{{StartTime}}", service.ServiceStartDate.ToString("HH:mm")),
                            new KeyValuePair<string, string>("{{EndTime}}", service.ServiceStartDate.AddHours(Convert.ToDouble(service.SubTotal)).ToString("HH:mm")),
                        }
                    };

                    await emailService.SendTestEmail(userEmailOptions);
                }
                return Json(new { reScheduleSuccess = true, serviceId = ServiceId, reScheduleDate = service.ServiceStartDate.ToString("dd-MM-yyyy").Replace('-', '/'), reScheduleTime = service.ServiceStartDate.ToString("HH:mm") + " to " + service.ServiceStartDate.AddHours(Convert.ToDouble(service.SubTotal)).ToString("HH:mm")});
            }

            return Json(new { reScheduleDataRequired = true });
        }

        public IActionResult FavouritePros()
        {
            int userId = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var FavouritePros = userRepository.GetFavouritePros(userId);
            List<FavouriteProsViewModel> favouriteViewModel = new List<FavouriteProsViewModel>();
            foreach(FavoriteAndBlocked favorite in FavouritePros)
            {
                FavouriteProsViewModel newObject = new FavouriteProsViewModel()
                {
                    Id = favorite.Id,
                    UserId = favorite.UserId,
                    TargetUserId = favorite.TargetUserId,
                    IsBlocked = favorite.IsBlocked,
                    IsFavourite = favorite.IsFavorite,
                    TargetUserName = favorite.TargetUser.FirstName + " " + favorite.TargetUser.LastName,
                    Ratings = favorite.TargetUser.RatingRatingToNavigations.Where(u => u.RatingTo == favorite.TargetUserId).Average(r => r.Ratings),
                };
                int cleanings = userRepository.GetNoOfCleanings(userId, favorite.TargetUserId);
                newObject.Cleanings = cleanings;
                favouriteViewModel.Add(newObject);
            }
            return View(favouriteViewModel);
        }

        [Route("User/Blockunblock/{ID}/{value}")]
        public string BlockUnblock(int Id, bool Value)
        {
            bool res = userRepository.Blockunblock(Id, Value);
            if (res)
                return "Unblock";
            else
                return "Block";
        }

        [Route("User/FavUnfav/{ID}/{value}")]
        public string FavUnfav(int Id, bool Value)
        {
            bool res = userRepository.FavUnfav(Id, Value);
            if (res)
                return "Remove";
            else
                return "Favourite";
        }

        public IActionResult ReportAnIssue(int serviceId)
        {
            return RedirectToAction("Contact", "Home", new { serviceId = serviceId });
        }

        [Route("User/GetUserWithServiceId/{serviceId}")]
        public async Task<IActionResult> GetUserWithServiceId(int serviceId)
        {
            ServiceRequest service = await userRepository.GetServiceWithUser(serviceId);
            ContactCreateViewModel reportModel = new ContactCreateViewModel()
            {
                ServiceId = service.ServiceRequestId,
                firstname = service.User.FirstName,
                lastname = service.User.LastName,
                PhoneNumber = service.User.Mobile,
                Email = service.User.Email
            };
            return Json(new { firstName = service.User.FirstName, lastName = service.User.LastName, mobile = service.User.Mobile, email = service.User.Email });
        }

        //public ServiceTimeConflict ChechScheduleConflict(DateTime dateTime, decimal totalHours, int spId)
        //{
        //    ICollection<ServiceRequest> spService = userRepository.GetSPService(spId);
        //    DateTime newStartTime = dateTime;
        //    DateTime newEndTime = dateTime.AddHours(Convert.ToDouble(totalHours));
        //    ServiceTimeConflict conflict;
        //    foreach (ServiceRequest service in spService)
        //    {
        //        DateTime startTime = service.ServiceStartDate.AddHours(-1);
        //        DateTime endTime = service.ServiceStartDate.AddHours(Convert.ToDouble(totalHours) + 1);

        //        if ((newStartTime <= startTime && newEndTime >= endTime) 
        //            || (newStartTime >= startTime && newStartTime <= endTime) 
        //            || (newEndTime >= startTime && newEndTime <= endTime))
        //        {
        //            conflict = new ServiceTimeConflict()
        //            {
        //                Conflict = true,
        //                ConflictDate = service.ServiceStartDate.ToString("dd-MM-yyyy"),
        //                ConflictStartTime = service.ServiceStartDate.ToString("HH:mm"),
        //                ConflictEndTime = service.ServiceStartDate.AddHours(Convert.ToDouble(service.SubTotal)).ToString("HH:mm"),
        //                ServiceId = service.ServiceRequestId
        //            };
        //            return conflict;
        //        }
        //    }
        //    conflict = new ServiceTimeConflict()
        //    {
        //        Conflict = false
        //    };
        //    return conflict;
        //}
    }
}
