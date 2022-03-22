using Helperland.IRepositories;
using Helperland.Models;
using Helperland.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//Service Status
//    new - 1
//    pending - 2 (AcceptedAtActionResult by SP)
//    completed - 3
//    canclled - 4
//    Cancelled by SP - 5

namespace Helperland.Controllers
{
    [Authorize(Roles = "1")]
    public class ServiceController : Controller
    {
        private readonly IServiceRequestRepository serviceRequest;
        private readonly IEmailService emailService;
        private readonly IUserRepository userRepository;

        public ServiceController(IServiceRequestRepository serviceRequest, IEmailService emailService, IUserRepository userRepository)
        {
            this.serviceRequest = serviceRequest;
            this.emailService = emailService;
            this.userRepository = userRepository;
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
        public async Task<IActionResult> BookService(ServiceRequestViewModel newRequest)
        {
            if (ModelState.IsValid)
            {
                int userId = Convert.ToInt32(HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);
                int ES = 0;
                ES = newRequest.ExtraService1 ? ES + 1 : ES + 0;
                ES = newRequest.ExtraService2 ? ES + 1 : ES + 0;
                ES = newRequest.ExtraService3 ? ES + 1 : ES + 0;
                ES = newRequest.ExtraService4 ? ES + 1 : ES + 0;
                ES = newRequest.ExtraService5 ? ES + 1 : ES + 0;

                ServiceRequest service = new ServiceRequest
                {
                    UserId = userId,
                    ServiceStartDate = Convert.ToDateTime(newRequest.Date + " " + newRequest.Time.ToString()),
                    ZipCode = newRequest.ZipCode,
                    Comments = newRequest.Comments,
                    HasPets = newRequest.HasPets,
                    PaymentDone = false,
                    CreatedDate = DateTime.Now,
                    ServiceHours = newRequest.ServiceHours,
                    ServiceHourlyRate = 18,
                    ExtraHours = ES * 0.5,
                    SubTotal = Convert.ToDecimal(newRequest.ServiceHours + (ES*0.5)),
                    TotalCost = Convert.ToDecimal((newRequest.ServiceHours + (ES*0.5)) * 18),
                    Status = 1
                };

                if (newRequest.FavouriteProsId != 0)
                {
                    service.ServiceProviderId = newRequest.FavouriteProsId;
                    service.Status = 2;
                    service.SpacceptedDate = DateTime.Now;
                }

                //Console.WriteLine(service.ExtraHours);
                //Console.WriteLine(service.SubTotal);
                //Console.WriteLine(service.TotalCost);
                UserAddress userAddress = serviceRequest.GetUserAddress(newRequest.AddressId);
                ServiceRequestAddress serviceAddress = new ServiceRequestAddress
                {

                    AddressLine1 = userAddress.AddressLine1,
                    AddressLine2 = userAddress.AddressLine2,
                    City = userAddress.City,
                    PostalCode = userAddress.PostalCode,
                    Mobile = userAddress.Mobile,
                    Email = userAddress.Email
                };
                service.ServiceRequestAddresses.Add(serviceAddress);
                if(newRequest.ExtraService1) service.ServiceRequestExtras.Add(new ServiceRequestExtra() {ServiceExtraId = 1 })  ;
                if(newRequest.ExtraService2) service.ServiceRequestExtras.Add(new ServiceRequestExtra() {ServiceExtraId = 2 })  ;
                if(newRequest.ExtraService3) service.ServiceRequestExtras.Add(new ServiceRequestExtra() {ServiceExtraId = 3 })  ;
                if(newRequest.ExtraService4) service.ServiceRequestExtras.Add(new ServiceRequestExtra() {ServiceExtraId = 4 })  ;
                if(newRequest.ExtraService5) service.ServiceRequestExtras.Add(new ServiceRequestExtra() {ServiceExtraId = 5 })  ;
                int serviceId = serviceRequest.AddServiceRequest(service);
                //serviceRequest.AddServiceReqAddress(serviceAddress);
                //Console.WriteLine("service id is: "+serviceId);



                User user = await userRepository.GetUser(userId);
                string ExtraServices = "";
                ExtraServices = ExtraServices + "<tr><td>Basic Service</td><td>" + service.ServiceHours + " Hrs </td><td>" + (service.ServiceHours * 18.00) + " €</td></tr>";
                ExtraServices = (newRequest.ExtraService1) ? ExtraServices + "<tr><td>Inside cabinets</td><td>0.5 Hrs</td><td>9.00 €</td></tr>" : ExtraServices + "";
                ExtraServices = (newRequest.ExtraService2) ? ExtraServices + "<tr><td>Inside fridge</td><td>0.5 Hrs</td><td>9.00 €</td></tr>" : ExtraServices + "";
                ExtraServices = (newRequest.ExtraService3) ? ExtraServices + "<tr><td>Inside oven</td><td>0.5 Hrs</td><td>9.00 €</td></tr>" : ExtraServices + "";
                ExtraServices = (newRequest.ExtraService5) ? ExtraServices + "<tr><td>Interio window</td><td>0.5 Hrs</td><td>9.00 €</td></tr>" : ExtraServices + "";
                ExtraServices = (newRequest.ExtraService4) ? ExtraServices + "<tr><td>Laundry wash & dry </td><td>0.5 Hrs</td><td>9.00 €</td></tr>" : ExtraServices + "";

                if(newRequest.FavouriteProsId != 0)
                {
                    User SP = await userRepository.GetUser(newRequest.FavouriteProsId);
                    Console.WriteLine(SP.Email);
                    NewReqEmailModel newReqEmail = new NewReqEmailModel
                    {
                            DateTime = service.ServiceStartDate,
                            TotalAmount = service.TotalCost,
                            ExtraServices = ExtraServices,
                            SPName = SP.FirstName + " " + SP.LastName,
                            UserName = user.FirstName + " " + user.LastName,
                            Email = SP.Email,
                            SPId = SP.UserId,
                            ServiceId = serviceId,
                            DirectAssigned = true
                    };

                    await userRepository.SendNewReqEmail(newReqEmail);
                }
                else
                {
                    IEnumerable<User> usersWithSameZipCode = serviceRequest.GetUserWithZipCode(service.ZipCode, service.HasPets);
                    var blockedSP = userRepository.GetBlocked(userId);
                    //foreach (User SP in usersWithSameZipCode)
                    //{
                    //    Console.WriteLine(SP.Email);
                    //    if (!blockedSP.Contains(SP.UserId))
                    //    {
                    //        NewReqEmailModel newReqEmail = new NewReqEmailModel
                    //        {
                    //            DateTime = service.ServiceStartDate,
                    //            TotalAmount = service.TotalCost,
                    //            ExtraServices = ExtraServices,
                    //            SPName = SP.FirstName + " " + SP.LastName,
                    //            UserName = user.FirstName + " " + user.LastName,
                    //            Email = SP.Email,
                    //            SPId = SP.UserId,
                    //            ServiceId = serviceId,
                    //            DirectAssigned = false
                    //        };

                    //        await userRepository.SendNewReqEmail(newReqEmail);
                    //    }
                    //}
                    NewReqEmailModel newReqEmail = new NewReqEmailModel
                    {
                        DateTime = service.ServiceStartDate,
                        TotalAmount = service.TotalCost,
                        ExtraServices = ExtraServices,
                        UserName = service.User.FirstName + " " + service.User.LastName,
                        Emails = new List<string>(),
                        ServiceId = service.ServiceRequestId
                    };
                    foreach (User SP in usersWithSameZipCode)
                    {
                        Console.WriteLine(SP.Email);
                        newReqEmail.Emails.Add(SP.Email);
                    }
                    await userRepository.SendNewReqEmail(newReqEmail);
                }
                

                ViewBag.submitted = 1;
                ModelState.Clear();
            }
            return View();
            
        }


        //---------------------------THIS FUNCTION ADDED IN UserRepository, It can be used from Service Controller and SP Controller----------------------
        //public async Task SendNewReqEmail(NewReqEmailModel newServiceEmail)
        //{
        //    UserEmailOptions userEmailOptions = new UserEmailOptions
        //    {
        //        ToEmails = new List<string>() { newServiceEmail.Email },
        //        templateName = "NewServiceRequest",
        //        Subject = "New Service Request",
        //        Placeholder = new List<KeyValuePair<string, string>>()
        //            {
        //                new KeyValuePair<string, string>("{{SPName}}", newServiceEmail.SPName),
        //                new KeyValuePair<string, string>("{{UserName}}", newServiceEmail.UserName),
        //                new KeyValuePair<string, string>("{{DateTime}}", newServiceEmail.DateTime.ToString()),
        //                new KeyValuePair<string, string>("{{Services}}", newServiceEmail.ExtraServices),
        //                new KeyValuePair<string, string>("{{TotalAmount}}", newServiceEmail.TotalAmount.ToString()),
        //                new KeyValuePair<string, string>("{{ServiceId}}", newServiceEmail.ServiceId.ToString()),
        //                //new KeyValuePair<string, string>("{{Id}}", newServiceEmail.SPId.ToString()),
        //            }
        //    };

        //    await emailService.SendTestEmail(userEmailOptions);
        //}

        //------------------------------------------------------------------------------------------------------------------------------------------------

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
            //var address = serviceRequest.GetUserAddresses(3021);
            var address = serviceRequest.GetUserAddresses(Convert.ToInt32(HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value));
            Console.WriteLine(address);
            return View(address);
            
        }

        public IActionResult GetFavouritePros()
        {
            int userId = Convert.ToInt32(HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);
            var favorite = userRepository.GetFavorites(userId);
            return View(favorite);
        }

        [HttpGet]
        [Route("Service/AddNewAddress/{zipCode}")]
        public IActionResult AddNewAddress(string zipCode)
        {
            Console.WriteLine("getting city name and New Address View");
            string city = serviceRequest.GetCityName(zipCode);
            UserAddressViewModel userAddress = new UserAddressViewModel
            {
                City = city,
                ZipCode = zipCode
            };
            Console.WriteLine(userAddress);
            return View(userAddress);
        }

        [HttpPost]
        public IActionResult AddNewAddress(UserAddressViewModel newAdd)
        {
            Console.WriteLine(newAdd.ZipCode);
            Console.WriteLine(newAdd.City);
            Console.WriteLine(newAdd.AddressLine1);
            if (ModelState.IsValid)
            {
                UserAddress newUserAddress = new UserAddress
                {
                    AddressLine1 = newAdd.AddressLine1,
                    AddressLine2 = newAdd.AddressLine2,
                    City = newAdd.City,
                    PostalCode = newAdd.ZipCode,
                    Mobile = newAdd.Mobile,
                    //Email = "test8@yopmail.com",
                    Email = HttpContext.User.FindFirst("Email").Value,
                    UserId = Convert.ToInt32(HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value),
                    IsDefault = false,
                    IsDeleted = false
                };

                serviceRequest.AddNewAddress(newUserAddress);
                return Json(new { newAddressAdded = true });
            }
            return Json(new { newAddressError = true, view = Helper.RenderRazorViewToString(this, "AddNewAddress", newAdd) }) ;
            
        }
    }
}
