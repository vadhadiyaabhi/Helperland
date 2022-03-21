using BC = BCrypt.Net.BCrypt;
using Helperland.IRepositories;
using Helperland.Models;
using Helperland.Repository;
using Helperland.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Implementations
{
    public class UserImplementation : IUserRepository
    {
        private AppDbContext DbContext;
        private readonly ICipherService cipherService;
        private readonly IEmailService emailService;

        public UserImplementation(AppDbContext dbContext, ICipherService cipherService, IEmailService emailService)
        {
            DbContext = dbContext;
            this.cipherService = cipherService;
            this.emailService = emailService;
        }

        public async Task<User> Activate(string cipher)
        {
            string token = cipherService.Decrypt(cipher);

            //----------------------------------------------------------
            //int delimeter = token.IndexOf(";", 37);
            //int length = delimeter - 37;
            //int id = int.Parse(token.Substring(37, length));
            //int timestamp = int.Parse( token.Substring(delimeter + 1));
            //--------------------------OR-------------------------------------
            string[] tokenArray = token.Split(";");
            int id = int.Parse(tokenArray[1]);
            int timestamp = int.Parse(tokenArray[2]);
            Console.WriteLine("\n token is: " + token);
            //Console.WriteLine("\n delimeter is: " + delimeter);
            Console.WriteLine("\n Id is: " + id);
            Console.WriteLine("\n Timestamp is: " + timestamp);



            //Using where clause---------------------------------------------
            //string Dbtoken = DbContext.Users.Where(u => u.UserId == id)
            //                             .Select(u => u.Token)
            //                             .SingleOrDefault();
            //----------------------------------------------------------------


            //to Find using primary key
            //string Dbtoken = DbContext.Users.Find(id).Token; 


            //To find withput Primary key
            //string Dbtoken = DbContext.Users.SingleOrDefault(user => user.UserId == id).Token;

            //User user = DbContext.Users.Find(id);
            User user = await GetUser(id);
            //DbContext.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            //Console.WriteLine("\n dbToken is: " + Dbtoken);
            if (user != null)
            {
                if (String.Equals(user.Token, cipher))
                {
                    int CurrentTimeStamp = GetCurrentTimeStamp();
                    if (timestamp - CurrentTimeStamp <= 86400)
                    {
                        //Console.WriteLine("\n Inside if");
                        user.IsActive = true;
                        DbContext.SaveChanges();
                    }
                    else
                    {
                        user.Token = null;
                        DbContext.SaveChanges();
                    }
                }
                else
                {
                    //return false;
                }
            }

            return user;

        }

        public int AddToken(int id, string token)
        {
            var user = new User()
            {
                UserId = id,
                Token = token
            };

            DbContext.Users.Attach(user);
            DbContext.Entry(user).Property(x => x.Token).IsModified = true;
            DbContext.SaveChanges();
            DbContext.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            return 1;
        }

        public string GenerateToken(int id)
        {
            string token = Guid.NewGuid().ToString() + ";" + id + ";" + Convert.ToString(GetCurrentTimeStamp());
            token = cipherService.Encrypt(token);
            //token = cipherService.Encrypt(token);
            //Console.WriteLine("\n" + token);
            //Console.WriteLine("\n Encrypted toke is " + token);
            //string newToken = cipherService.Decrypt(token);
            //Console.WriteLine("Decrypted token is  " + newToken);


            //---------------------------------------------------------------------------------------
            //byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            //byte[] key = Guid.NewGuid().ToByteArray();
            //string token = Convert.ToBase64String(time.Concat(key).ToArray());

            //string token = "4qS7ISTq2UhCFwi5wlyxSaUNjUg8GkB/";
            //byte[] data = Convert.FromBase64String(token);
            //Console.WriteLine("before " + token);
            //DateTime when = DateTime.FromBinary(BitConverter.ToInt64(data, 0));
            //Console.WriteLine("after " + when);
            //---------------------------------------------------------------------------------------------
            return token;
        }

        public User Register(User user)
        { 
            DbContext.Users.Add(user);
            DbContext.SaveChanges();
            DbContext.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            return user;
        }


        public async Task<bool> ResetForgotPassowrd(ForgotPasswordViewModel forgotPassword)
        {
            string token = cipherService.Decrypt(forgotPassword.Token);
            string[] TokenArray = token.Split(";");
            int Id = int.Parse(TokenArray[1]);
            int timeStamp = int.Parse(TokenArray[2]);

            User user =await GetUser(Id);
            if (String.Equals(user.Token, forgotPassword.Token))
            {
                if (GetCurrentTimeStamp() - timeStamp <= 3600)
                {
                    user.Password = BC.HashPassword(forgotPassword.Password);
                    user.Token = null;
                    DbContext.SaveChanges();
                    return true;
                }
                else
                {
                    user.Token = null;
                    return false;
                }
            }

            return false;
                
        }

        public int GetCurrentTimeStamp()
        {
            return (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }

        public async Task<User> GetUser(int Id)
        {
            return await DbContext.Users.FindAsync(Id);
        }

        public UserAddress GetSPAddress(int userId)
        {
            return DbContext.UserAddresses.Where(add => add.UserId == userId).FirstOrDefault();
        }

        public async Task<User> UpdateUser(UserUpdateViewModel userViewModel, int userId)
        {
            User user = await DbContext.Users.FindAsync(userId);
            user.FirstName = userViewModel.FirstName;
            user.LastName = userViewModel.LastName;
            if(userViewModel.BirthDate != null && userViewModel.BirthMonth != null && userViewModel.BirthYear != null)
                user.DateOfBirth = new DateTime(Convert.ToInt32(userViewModel.BirthYear), Convert.ToInt32(userViewModel.BirthMonth), Convert.ToInt32(userViewModel.BirthDate));
            user.Mobile = userViewModel.Mobile;
            user.ModifiedDate = userViewModel.ModifiedDate;
            user.ModifiedBy = userId;
            user.LanguageId = userViewModel.LanguageId;
            await DbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateSp(SPUpdateViewModel spModel, int spId)
        {
            User sp = await DbContext.Users.FindAsync(spId);
            sp.FirstName = spModel.FirstName;
            sp.LastName = spModel.LastName;
            sp.Mobile = spModel.Mobile;
            if (spModel.BirthDate != null && spModel.BirthMonth != null && spModel.BirthYear != null)
                sp.DateOfBirth = new DateTime(Convert.ToInt32(spModel.BirthYear), Convert.ToInt32(spModel.BirthMonth), Convert.ToInt32(spModel.BirthDate));
            sp.ModifiedDate = spModel.ModifiedDate;
            sp.ModifiedBy = spId;
            sp.ZipCode = spModel.ZipCode;
            sp.NationalityId = spModel.NationalityId;
            sp.Gender = spModel.Gender;
            sp.WorksWithPets = spModel.WorksWithPet;
            await DbContext.SaveChangesAsync();
            return sp;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            //User user = DbContext.Users.Where(user => user.Email == email).FirstOrDefault();
            User user =await DbContext.Users.FirstOrDefaultAsync(user => user.Email == email);
            if(user != null)
            {
                DbContext.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            }
            return user;
        }

        public async Task<bool> DeleteAddress(int addressId)
        {
            UserAddress userAddress = await DbContext.UserAddresses.FindAsync(addressId);
            if(userAddress != null)
            {
                DbContext.UserAddresses.Remove(userAddress);
                DbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<bool> ResetPassword(ResetPasswordViewModel resetPassword, int userId)
        {
            User user = await DbContext.Users.FindAsync(userId);
            if (user != null)
            {
                if (BC.Verify(resetPassword.OldPassword, user.Password))
                {
                    user.Password = BC.HashPassword(resetPassword.Password);
                    user.ModifiedDate = resetPassword.ModifiedDate;
                    user.ModifiedBy = userId;
                    DbContext.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
            else 
                return false;
        }

        public async Task<bool> UpdateUserAddress(UserAddressViewModel userAddress)
        {
            UserAddress newAddress = await DbContext.UserAddresses.FindAsync(userAddress.AddressId);
            if(newAddress != null)
            {
                newAddress.AddressLine1 = userAddress.AddressLine1;
                newAddress.AddressLine2 = userAddress.AddressLine2;
                newAddress.Mobile = userAddress.Mobile;
                newAddress.City = userAddress.City;
                newAddress.PostalCode = userAddress.ZipCode;
                await DbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateSpAddress(SPUpdateViewModel userAddress)
        {
            UserAddress newAddress = await DbContext.UserAddresses.FindAsync(userAddress.AddressId);
            if (newAddress != null)
            {
                newAddress.AddressLine1 = userAddress.AddressLine1;
                newAddress.AddressLine2 = userAddress.AddressLine2;
                newAddress.Mobile = userAddress.Mobile;
                newAddress.City = userAddress.City;
                newAddress.PostalCode = userAddress.ZipCode;
                await DbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public IEnumerable<ServiceRequest> GetCurrentServices(int userId)
        {
            return DbContext.ServiceRequests.Include(x => x.ServiceProvider)
                .ThenInclude(x => x.RatingRatingToNavigations).AsSplitQuery()
                .Where(u => u.UserId == userId && u.Status != 3 && u.Status != 4)
                .Select(s => new ServiceRequest
                {
                    ServiceRequestId = s.ServiceRequestId,
                    TotalCost = s.TotalCost,
                    SubTotal = s.SubTotal,
                    ServiceProviderId = s.ServiceProviderId,
                    ServiceStartDate = s.ServiceStartDate,
                    ServiceProvider = s.ServiceProvider,
                    Ratings = s.ServiceProvider.RatingRatingToNavigations
                }).ToList();

            //return (from service in DbContext.ServiceRequests
            //        where service.UserId == userId
            //        join sp in DbContext.Users on service.ServiceProviderId equals sp.UserId
            //        select (new CurrentService
            //        {

            //        }));
        }

        public async Task<int> DeleteServiceRequest(int ServiceReqId)
        {
            ServiceRequest service = await DbContext.ServiceRequests.FindAsync(ServiceReqId);
            service.Status = 4;
            DbContext.SaveChanges();
            return Convert.ToInt32(service.Status);
        }

        public async Task<ServiceRequest> GetServiceDetails(int ServiceId)
        {
            ServiceRequest service = await DbContext.ServiceRequests.Include(service => service.ServiceProvider)
                                            .Include(service => service.User)
                                            .Include(service => service.ServiceRequestAddresses).AsSplitQuery()
                                            .Include(service => service.ServiceRequestExtras).AsSplitQuery()
                                            .Where(service => service.ServiceRequestId == ServiceId)
                                            .FirstOrDefaultAsync();
        //https://docs.microsoft.com/en-us/ef/core/querying/single-split-queries
            return service;
        }

        public IEnumerable<ServiceRequest> GetUserServiceHistory(int userId)
        {
            return DbContext.ServiceRequests.Include(x => x.ServiceProvider)
                                            .ThenInclude(sp => sp.RatingRatingToNavigations).AsSplitQuery()
                                            .Where(s => s.UserId == userId && (s.Status == 3 || s.Status == 4))
                                            .Select(s => new ServiceRequest
                                            {
                                                ServiceRequestId = s.ServiceRequestId,
                                                TotalCost = s.TotalCost,
                                                SubTotal = s.SubTotal,
                                                ServiceProviderId = s.ServiceProviderId,
                                                ServiceStartDate = s.ServiceStartDate,
                                                ServiceProvider = s.ServiceProvider,
                                                //new User { UserId = s.ServiceProvider.UserId, FirstName = s.ServiceProvider.FirstName, LastName = s.ServiceProvider.LastName },
                                                Ratings = s.ServiceProvider.RatingRatingToNavigations,
                                                Status = s.Status
                                            })
                                            .ToList();
        }

        public async Task<bool> AddRating(Rating rating)
        {
            await DbContext.Ratings.AddAsync(rating);
            await DbContext.SaveChangesAsync();
            if (rating.RatingId != 0)
                return true;
            else
                return false;
        }

        public async Task<ServiceRequest> GetServiceWithUser(int ServiceId)
        {
            return await DbContext.ServiceRequests.Include(x => x.User)
                                    .Select(s => new ServiceRequest
                                    {
                                        ServiceRequestId = s.ServiceRequestId,
                                        //ServiceProvider = new User { UserId = s.ServiceProvider.UserId, FirstName = s.ServiceProvider.FirstName, LastName = s.ServiceProvider.LastName, Email = s.ServiceProvider.Email}
                                        User = s.User
                                    }).Where(s => s.ServiceRequestId == ServiceId).FirstOrDefaultAsync();
        }

        public async Task<ServiceRequest> GetService(int ServiceId)
        {
            return await DbContext.ServiceRequests.Include(x => x.ServiceProvider)
                                    .Select(s => new ServiceRequest
                                    {
                                        ServiceRequestId = s.ServiceRequestId,
                                        ServiceStartDate = s.ServiceStartDate,
                                        ServiceProviderId = s.ServiceProviderId,
                                        SubTotal = s.SubTotal,
                                        TotalCost = s.TotalCost,
                                        Status = s.Status,
                                        ZipCode = s.ZipCode,
                                        //ServiceProvider = new User { UserId = s.ServiceProvider.UserId, FirstName = s.ServiceProvider.FirstName, LastName = s.ServiceProvider.LastName, Email = s.ServiceProvider.Email}
                                        ServiceProvider = s.ServiceProvider
                                    }).Where(s => s.ServiceRequestId == ServiceId).FirstOrDefaultAsync();
        }

        public ICollection<ServiceRequest> GetSPService(int spId)
        {
            return DbContext.ServiceRequests.Where(x => x.ServiceProviderId == spId && x.Status == 2)
                                            .Select(s => new ServiceRequest
                                            {
                                                ServiceRequestId = s.ServiceRequestId,
                                                ServiceStartDate = s.ServiceStartDate,
                                                SubTotal = s.SubTotal,
                                                ServiceProviderId = s.ServiceProviderId,
                                                UserId = s.UserId
                                            })
                                            .ToList();
        }

        public async Task<ServiceRequest> ReScheduleService(int ServiceId, DateTime dateTime)
        {
            ServiceRequest service = await DbContext.ServiceRequests.FindAsync(ServiceId);
            service.ServiceStartDate = dateTime;
            service.ModifiedDate = DateTime.Now;
            service.ModifiedBy = service.UserId;
            DbContext.SaveChanges();
            return service;
        }

        public ServiceTimeConflict ChechScheduleConflict(DateTime dateTime, decimal totalHours, int spId)
        {
            ICollection<ServiceRequest> spService = GetSPService(spId);
            DateTime newStartTime = dateTime;
            DateTime newEndTime = dateTime.AddHours(Convert.ToDouble(totalHours));
            ServiceTimeConflict conflict;
            foreach (ServiceRequest service in spService)
            {
                DateTime startTime = service.ServiceStartDate.AddHours(-1);
                DateTime endTime = service.ServiceStartDate.AddHours(Convert.ToDouble(service.SubTotal) + 1);
                Console.WriteLine(startTime + " to " + endTime);

                if ((newStartTime < startTime && newEndTime > endTime)
                    || (newStartTime > startTime && newStartTime < endTime)
                    || (newEndTime > startTime && newEndTime < endTime))
                {
                    conflict = new ServiceTimeConflict()
                    {
                        Conflict = true,
                        ConflictDate = service.ServiceStartDate.ToString("dd-MM-yyyy"),
                        ConflictStartTime = service.ServiceStartDate.ToString("HH:mm"),
                        ConflictEndTime = service.ServiceStartDate.AddHours(Convert.ToDouble(service.SubTotal)).ToString("HH:mm"),
                        ServiceId = service.ServiceRequestId
                    };
                    return conflict;
                }
            }
            conflict = new ServiceTimeConflict()
            {
                Conflict = false
            };
            return conflict;
        }

        public IEnumerable<ServiceRequest> GetNewServiceRequests(int spId)
        {
            var spBlockedByUser = DbContext.FavoriteAndBlockeds.Where(x => x.TargetUserId == spId && x.IsBlocked == true).Select(x => x.UserId).ToArray();
            var blockedUser = DbContext.FavoriteAndBlockeds.Where(x => x.UserId == spId && x.IsBlocked == true).Select(x => x.TargetUserId).ToArray();
            string zipCode = DbContext.Users.Where(u => u.UserId == spId).Select(x => x.ZipCode).FirstOrDefault();
            return DbContext.ServiceRequests.Include(u => u.User).Include(sp => sp.ServiceRequestAddresses).AsSplitQuery().Where(s => s.ZipCode == zipCode && (s.Status == 1 || s.Status == 5) &&  !blockedUser.Contains(s.UserId) && !spBlockedByUser.Contains(s.UserId)).ToList();
        }

        public async Task<ServiceRequest> AcceptService(int ServiceId, int spId)
        {
            ServiceRequest service = await DbContext.ServiceRequests.FindAsync(ServiceId);
            service.ServiceProviderId = spId;
            service.SpacceptedDate = DateTime.Now;
            service.Status = 2;
            await DbContext.SaveChangesAsync();
            return service;
        }

        public IEnumerable<ServiceRequest> GetSPServiceHistory(int spId)
        {
            return DbContext.ServiceRequests.Include(x => x.User)
                                            .Include(x => x.ServiceRequestAddresses).AsSingleQuery()
                                            .Where(s => s.ServiceProviderId == spId && (s.Status == 3)).ToList();
        }

        public IEnumerable<ServiceRequest> GetSPUpcomingServices(int spId)
        {
            return DbContext.ServiceRequests.Include(x => x.User)
                                            .Include(x => x.ServiceRequestAddresses).AsSplitQuery()
                                            .Where(s => s.ServiceProviderId == spId && s.Status == 2).ToList();
        }

        public async Task<bool> MarkAsCompleted(int ServiceId, int spId)
        {
            ServiceRequest service = await DbContext.ServiceRequests.FindAsync(ServiceId);
            service.Status = 3;
            service.ModifiedDate = DateTime.Now;
            service.ModifiedBy = spId;
            await DbContext.SaveChangesAsync();
            int favAndBlockCount = DbContext.FavoriteAndBlockeds.Where(x => x.UserId == service.UserId && x.TargetUserId == spId).Count();
            if(favAndBlockCount == 0)
            {
                FavoriteAndBlocked newObject = new FavoriteAndBlocked()
                {
                    UserId = service.UserId,
                    TargetUserId = spId,
                    IsBlocked = false,
                    IsFavorite = false,
                };
                FavoriteAndBlocked newObject2 = new FavoriteAndBlocked()
                {
                    UserId = spId,
                    TargetUserId = service.UserId,
                    IsBlocked = false,
                    IsFavorite = false,
                };
                DbContext.FavoriteAndBlockeds.Add(newObject);
                DbContext.FavoriteAndBlockeds.Add(newObject2);
                await DbContext.SaveChangesAsync();
            }
            return true;
        }

        public async Task<ServiceRequest> CancelServiceBySP(int serviceId, int spId)
        {
            ServiceRequest service = await DbContext.ServiceRequests.FindAsync(serviceId);
            //if(service.ServiceStartDate < DateTime.Now)
            //{
            //    service.Status = 4;
            //}
            //else
            //{
                service.Status = 5;
            //}
            service.ServiceProviderId = null;
            service.ModifiedDate = DateTime.Now;
            service.ModifiedBy = spId;
            await DbContext.SaveChangesAsync();
            return service;
        }

        public IEnumerable<Rating> MyRatings(int spId)
        {
            return DbContext.Ratings.Include(x => x.RatingFromNavigation)
                                    .Include(x => x.ServiceRequest).AsSplitQuery()
                                    .Where(r => r.RatingTo == spId).ToList();
        }

        //----------------NOT IN USE------------------------Just to check EF Core functions
        public IEnumerable<int> Dummy(IEnumerable<Int32> spId)
        {
            return DbContext.ServiceRequests.Where(s => s.ServiceProviderId == 2 && !spId.Contains(s.UserId)).Select(s => s.UserId).Distinct();
        }
        //--------------------------------------------------------------------------------------

        public IEnumerable<FavoriteAndBlocked> GetBlockedBySP(int spId)
        {
            //var usersFromBlocked = DbContext.FavoriteAndBlockeds.Where(u => u.UserId == spId).Select(u => u.TargetUserId).ToArray();
            //foreach(int id in usersFromBlocked)
            //{
            //    //Console.WriteLine(id);
            //}
            //var usersWithSP = DbContext.ServiceRequests.Where(s => s.ServiceProviderId == spId && s.Status == 3 && !usersFromBlocked.Contains(s.UserId)).Select(u => u.UserId).Distinct().ToArray();
            //foreach (int targetUserId in usersWithSP)
            //{
            //    //Console.WriteLine(targetUserId);
            //    FavoriteAndBlocked newObject = new FavoriteAndBlocked()
            //    {
            //        UserId = spId,
            //        TargetUserId = targetUserId,
            //        IsBlocked = false,
            //        IsFavorite = false
            //    };

            //    DbContext.FavoriteAndBlockeds.Add(newObject);
            //    DbContext.SaveChanges();
            //}

            return DbContext.FavoriteAndBlockeds.Include(u => u.TargetUser).Where(u => u.UserId == spId).ToList();

        }


        public IEnumerable<FavoriteAndBlocked> GetFavouritePros(int userId)
        {
            //var usersFromFavouritePros = DbContext.FavoriteAndBlockeds.Where(u => u.UserId == userId).Select(u => u.TargetUserId).ToArray();
            //var spWithUser = DbContext.ServiceRequests.Where(s => s.UserId == userId && s.Status == 3).Select(u => u.ServiceProviderId).Distinct().ToArray();
            //List<int> newSp = new List<int>();
            //foreach(int id in spWithUser)
            //{
            //    if (!usersFromFavouritePros.Contains(id))
            //        newSp.Add(id);
            //}
            //foreach (int targetUserId in newSp)
            //{
            //    //Console.WriteLine(targetUserId);
            //    FavoriteAndBlocked newObject = new FavoriteAndBlocked()
            //    {
            //        UserId = userId,
            //        TargetUserId = targetUserId,
            //        IsBlocked = false,
            //        IsFavorite = false
            //    };

            //    DbContext.FavoriteAndBlockeds.Add(newObject);
            //    DbContext.SaveChanges();
            //}

            return DbContext.FavoriteAndBlockeds.Include(u => u.TargetUser)
                                                .ThenInclude(r => r.RatingRatingToNavigations)
                                                .Where(u => u.UserId == userId).ToList();

        }

        public bool Blockunblock(int Id, bool Value)
        {
            FavoriteAndBlocked FavAndBlock = DbContext.FavoriteAndBlockeds.Find(Id);
            FavAndBlock.IsBlocked = !Value;
            DbContext.SaveChanges();
            return FavAndBlock.IsBlocked;
        }

        public bool FavUnfav(int Id, bool Value)
        {
            FavoriteAndBlocked FavAndBlock = DbContext.FavoriteAndBlockeds.Find(Id);
            FavAndBlock.IsFavorite = !Value;
            DbContext.SaveChanges();
            return FavAndBlock.IsFavorite;
        }

        public int GetNoOfCleanings(int userId, int spId)
        {
            return DbContext.ServiceRequests.Where(s => s.UserId == userId && s.ServiceProviderId == spId && s.Status == 3).Count();
        }

        public IEnumerable<int> GetBlocked(int Id)
        {
            return DbContext.FavoriteAndBlockeds.Where(x => x.TargetUserId == Id && x.IsBlocked == true).Select(x => x.UserId).ToArray();
        }

        public IEnumerable<FavoriteAndBlocked> GetFavorites(int userId)
        {
            return DbContext.FavoriteAndBlockeds.Include(x => x.TargetUser).Where(x => x.UserId == userId && x.IsFavorite == true).ToList();
        }

        public async Task<bool> HasIssue(int serviceId)
        {
            ServiceRequest service = await DbContext.ServiceRequests.FindAsync(serviceId);
            service.HasIssue = true;
            await DbContext.SaveChangesAsync();
            return true;
        }

        public IEnumerable<User> GetOtherSPs(int spId, string zipCode)
        {
            return DbContext.Users.Where(u => u.UserTypeId == 2 && u.UserId != spId && u.ZipCode == zipCode).ToList();
        }

        public async Task SendNewReqEmail(NewReqEmailModel newServiceEmail)
        {
            UserEmailOptions userEmailOptions = new UserEmailOptions
            {
                ToEmails = new List<string>() { newServiceEmail.Email },
                templateName = "NewServiceRequest",
                Subject = "New Service Request",
                Placeholder = new List<KeyValuePair<string, string>>()
                    {
                        //new KeyValuePair<string, string>("{{SPName}}", newServiceEmail.SPName),
                        new KeyValuePair<string, string>("{{UserName}}", newServiceEmail.UserName),
                        new KeyValuePair<string, string>("{{DateTime}}", newServiceEmail.DateTime.ToString()),
                        new KeyValuePair<string, string>("{{Services}}", newServiceEmail.ExtraServices),
                        new KeyValuePair<string, string>("{{TotalAmount}}", newServiceEmail.TotalAmount.ToString()),
                        new KeyValuePair<string, string>("{{ServiceId}}", newServiceEmail.ServiceId.ToString()),
                        //new KeyValuePair<string, string>("{{Id}}", newServiceEmail.SPId.ToString()),
                    }
            };
            if (newServiceEmail.DirectAssigned)
            {
                userEmailOptions.templateName = "DirectAssignedServiceRequest";
            }

            await emailService.SendTestEmail(userEmailOptions);
        }
    }

}
