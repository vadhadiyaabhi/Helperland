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

        public UserImplementation(AppDbContext dbContext, ICipherService cipherService)
        {
            DbContext = dbContext;
            this.cipherService = cipherService;
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
            sp.NationalityId = spModel.NationalityId;
            sp.Gender = spModel.Gender;
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
            return DbContext.ServiceRequests.Include(x => x.ServiceProvider).ThenInclude(x => x.RatingRatingToNavigations).Where(u => u.UserId == userId && u.Status != 3 && u.Status != 4)
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
                                            .Where(s => s.UserId == userId && (s.Status == 3 || s.Status == 4)).ToList();
        }

        public async Task<bool> AddRating(Rating rating)
        {
            await DbContext.Ratings.AddAsync(rating);
            if (rating.RatingId != 0)
                return true;
            else
                return false;
        }
    }

}
