using Helperland.IRepositories;
using Helperland.Models;
using Helperland.Repository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Implementations
{
    public class UserImplementation : IUserRepository
    {
        private AppDbContext DbContext;
        public UserImplementation(AppDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public int Activate(string token)
        {
            //write a code to check token
            return 0;
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

            //string[] tokenArray = token.Split(';');
            //Also complete password match with confirm password

            return 1;

        }

        public string GenerateToken(int id)
        {
            var timeStamp = Convert.ToString((int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
            string token = Guid.NewGuid().ToString() + ";" + id + ";" + timeStamp;
            //string token = timeStamp;
            return token;
        }

        public User Register(User user)
        {
            DbContext.Users.Add(user);
            DbContext.SaveChanges();
            DbContext.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            return user;
        }
    }
}
