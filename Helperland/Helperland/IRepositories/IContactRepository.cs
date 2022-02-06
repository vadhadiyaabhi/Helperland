using Helperland.Models;
using Helperland.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.IRepositories
{
    public interface IContactRepository
    {
        public ContactU Create(ContactU contactU);
    }
}
