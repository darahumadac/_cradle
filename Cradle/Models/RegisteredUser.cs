using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cradle.Models.Enums;
using Cradle.Models.Users;

namespace Cradle.Models
{
    public abstract class RegisteredUser
    {
        public Role Role { get; set; }
        public Account AccountDetails { get; set; }
        public PersonalProfile PersonalProfile { get; set; }
    }
}