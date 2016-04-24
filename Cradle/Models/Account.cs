using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Cradle.Models.Enums;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Cradle.Models
{
    public class Account : IdentityUser
    {
        public string EmailAddress { get; set; }
        public int SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; } //should be in hash form
        public bool IsActive { get; set; }
        public int FailedLoginCount { get; set; }

        public virtual PersonalProfile PersonalProfile { get; set; }
        public virtual DesignerProfile DesignerProfile { get; set; }
        public virtual List<Order> Orders { get; set; }
 
    }

    public class CradleDbContext : IdentityDbContext<Account>
    {
        public CradleDbContext() : 
            base("CradleDBConnection")
        { 
        }

        public DbSet<SecurityQuestions> SecurityQuestions { get; set; }
        public DbSet<DesignerProfile> DesignerProfiles { get; set; }
        public DbSet<PersonalProfile> PersonalProfiles { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<ContactNumber> ContactNumbers { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>(); 
            
            //Account Table
            modelBuilder.Entity<Account>()
                .HasOptional(a => a.DesignerProfile)
                .WithRequired(dp => dp.Account);
        }
      
        
    }


}
