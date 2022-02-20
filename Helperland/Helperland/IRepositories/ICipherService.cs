using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.IRepositories
{
    public interface ICipherService
    {
        public string Encrypt(string input);
        public string Decrypt(string cipherText);
    }
}
