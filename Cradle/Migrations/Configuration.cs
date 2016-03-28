namespace Cradle.Migrations
{
    using Cradle.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Cradle.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            ApplicationUser[] mockUsers = new ApplicationUser[10];
            for (int i = 0; i < 5; i++ )
            {
                mockUsers[i] = new ApplicationUser
                {
                    UserName = "Member" + (i+1),
                    PasswordHash = "ACgshxKeZhQ+9tEDhtwIpZIgFnwb3793G6FzWEimyU/HUFsq9xDNBCZHJsAKE12vyw==",
                    FirstName = "Member",
                    LastName = "LastName"+(i+1),
                    City = "Manila",
                    Country = "PH",
                    MobileNo = "12345678901",
                    MemberAccountType = AccountType.Member
                };
            }
            for (int i = 5; i < 10; i++)
            {
                mockUsers[i] = new ApplicationUser
                {
                    UserName = "Designer" + (i + 1),
                    PasswordHash = "ACgshxKeZhQ+9tEDhtwIpZIgFnwb3793G6FzWEimyU/HUFsq9xDNBCZHJsAKE12vyw==",
                    FirstName = "Designer",
                    LastName = "LastName" + (i + 1),
                    City = "Manila",
                    Country = "PH",
                    MobileNo = "0987654321",
                    MemberAccountType = AccountType.Designer
                };
            }

            context.Users.AddOrUpdate(mockUsers);
            
        }
    }
}
