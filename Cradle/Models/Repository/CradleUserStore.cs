using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Cradle.Models.Repository
{
    public class CradleUserStore : UserStore<Account>
    {
        private CradleDbContext _context;

        public CradleUserStore(CradleDbContext context) : base(context)
        {
            _context = context;
        }

        public void AddDesignerProfile(DesignerProfile desginerProfiles)
        {

            _context.DesignerProfiles.Add(desginerProfiles);
            _context.SaveChanges();
        }

        public void AddPersonalProfile(PersonalProfile personalProfile)
        {

            _context.PersonalProfiles.Add(personalProfile);
            _context.SaveChanges();

            
        }

        public void AddAddress(Address address)
        {
            _context.Addresses.Add(address);
            _context.SaveChanges();
        }


        public void AddContactNo(ContactNumber personalContact)
        {
            _context.ContactNumbers.Add(personalContact);
            _context.SaveChanges();
        }

        public DbSet<SecurityQuestions> GetSecurityQuestions()
        {
            return _context.SecurityQuestions;
        }
    }
}