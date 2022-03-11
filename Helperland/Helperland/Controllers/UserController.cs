using Helperland.IRepositories;
using Helperland.Models;
using Helperland.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Helperland.Controllers
{
    public class UserController : Controller
    {
        private readonly IServiceRequestRepository serviceRepository;
        private readonly IUserRepository userRepository;

        public UserController(IServiceRequestRepository serviceRepository, IUserRepository userRepository)
        {
            this.serviceRepository = serviceRepository;
            this.userRepository = userRepository;
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
            if (ModelState.IsValid)
            {
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

        public async Task<IActionResult> DeleteServiceRequest(int ServiceRequestId, string cancleMessage)
        {
            Console.WriteLine(cancleMessage);
            int res = await userRepository.DeleteServiceRequest(ServiceRequestId);
            return Json(new { serviceDeleted = res, serviceId = ServiceRequestId });
        }
    }
}
