using Helperland.IRepositories;
using Helperland.Models;
using Helperland.Repository;
using Helperland.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Implementations
{
    public class ContactImplementation : IContactRepository
    {
        private readonly AppDbContext dbContext;
        public ContactImplementation(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //public ContactCreateViewModel Create(ContactCreateViewModel contactU)
        //{
        //    ContactU contact = new ContactU
        //    {
        //        Name = contactU.firstname + " " + contactU.lastname,
        //        Email = contactU.Email,
        //        Subject = contactU.Subject,
        //        Message = contactU.Message,
        //        PhoneNumber = contactU.PhoneNumber,
        //        UploadFileName = contactU.PhotoPath,
        //        CreatedOn = DateTime.Now
        //    };

        //    dbContext.ContactUs.Add(contact);
        //    dbContext.SaveChanges();
        //    contactU.submitted = 1;
        //    return contactU;
        //}
        public ContactU Create(ContactU contact)
        {

            dbContext.ContactUs.Add(contact);
            dbContext.SaveChanges();
            return contact;
        }
    }
}
