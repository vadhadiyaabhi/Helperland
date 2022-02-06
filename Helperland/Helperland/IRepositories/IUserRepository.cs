using Helperland.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.IRepositories
{
    public interface IUserRepository
    {
        public User Register(User user);
        public string GenerateToken(int id);
        public int AddToken(int id, string token);
        int Activate(string token);
    }
}
