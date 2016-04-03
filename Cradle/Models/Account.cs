using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Cradle.Models.Enums;

namespace Cradle.Models
{
    public class Account : IdentityUser
    {
        public Role Role { get; set; }
        public string EmailAddress { get; set; }
        public int SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; } //should be in hash form
        public bool IsActive { get; set; }
        public int FailedLoginCount { get; set; }
        
        public virtual RegisteredUser User { get; set; }
    }

    public class CradleDbContext : IdentityDbContext<Account>
    {
        public CradleDbContext() : 
            base("CradleDBConnection")
        { 
        }

    }


}
