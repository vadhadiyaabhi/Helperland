using Helperland.IRepositories;
using Microsoft.AspNetCore.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Implementations
{
    public class CipherService : ICipherService
    {
        private readonly IDataProtectionProvider dataProtectionProvider;
        private const string Key = "My-key-to-encrypt-token";

        public CipherService(IDataProtectionProvider dataProtectionProvider)
        {
            this.dataProtectionProvider = dataProtectionProvider;
        }

        public string Encrypt(string input)
        {
            var protector = dataProtectionProvider.CreateProtector(Key);
            return protector.Protect(input);
        }

        public string Decrypt(string cipherText)
        {
            var protector = dataProtectionProvider.CreateProtector(Key);
            return protector.Unprotect(cipherText);
        }

    }
}
