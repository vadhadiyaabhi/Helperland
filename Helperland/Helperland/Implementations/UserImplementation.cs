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

        public async Task<User> UpdateUser(UserUpdateViewModel userViewModel)
        {
            User user = await DbContext.Users.FindAsync(5024);
            user.FirstName = userViewModel.FirstName;
            user.LastName = userViewModel.LastName;
            user.DateOfBirth = userViewModel.DateOfBirth;
            user.Mobile = userViewModel.Mobile;
            DbContext.SaveChanges();
            return user;
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
    }
}
