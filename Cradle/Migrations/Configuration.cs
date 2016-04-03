namespace Cradle.Migrations
{
    using Cradle.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CradleDbContext> //DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(CradleDbContext context)
        {
            Account[] accounts = new Account[6];

            for (int i = 0; i < 3; i++)
            {
                accounts[i] = new Account
                {
                    UserName = "Member" + (i + 1),
                    PasswordHash = "ACgshxKeZhQ+9tEDhtwIpZIgFnwb3793G6FzWEimyU/HUFsq9xDNBCZHJsAKE12vyw==",
                    EmailAddress = "test@test.test" + (i + 1),
                    SecurityQuestion = 1,
                    SecurityAnswer = "test_answer",
                    IsActive = true,
                    FailedLoginCount = 0
                };

                //List<RegisteredUser> members = new List<RegisteredUser>
                //{
                //    new Member {
                //        RegisteredUserID = "ed179bb3-6669-4f31-92d5-e87a7a9ac8b6",
                //        Role = Models.Enums.Role.Member
                //    },
                //     new Member {
                //        RegisteredUserID = "0d043661-cbe0-4985-b21c-fd8f9236887a",
                //        Role = Models.Enums.Role.Member
                //    },
                //    new Member {
                //        RegisteredUserID = "b2dc333c-bfdc-4541-82a8-4a5b0ff6833f",
                //        Role = Models.Enums.Role.Member
                //    }
                //};

                //members.ForEach(m => context.Members.AddOrUpdate(m));
            }

            for (int i = 3; i < 6; i++)
            {
                accounts[i] = new Account
                {
                    UserName = "Designer" + (i + 1),
                    PasswordHash = "ACgshxKeZhQ+9tEDhtwIpZIgFnwb3793G6FzWEimyU/HUFsq9xDNBCZHJsAKE12vyw==",
                    EmailAddress = "Designertest@test.test" + (i + 1),
                    SecurityQuestion = 1,
                    SecurityAnswer = "test_answer",
                    IsActive = true,
                    FailedLoginCount = 0
                };

                
            }

            context.Users.AddOrUpdate(accounts);

           
        }
    }
}
