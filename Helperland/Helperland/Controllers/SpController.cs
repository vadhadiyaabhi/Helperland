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
    public class SPController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly IServiceRequestRepository serviceRepository;

        public SPController(IUserRepository userRepository, IServiceRequestRepository serviceRepository)
        {
            this.userRepository = userRepository;
            this.serviceRepository = serviceRepository;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MyAccount()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Mydetails()
        {
            int spId = Convert.ToInt32(HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);
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
                int userId = Convert.ToInt32(HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);
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


        //Not in use now----------------------------------------------------------------
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
    }
}
