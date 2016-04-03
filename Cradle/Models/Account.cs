using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cradle.Models
{
    public class Account
    {
        public int AccountID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } //in hash form
        public string EmailAddress { get; set; }
        public int SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; } //in hash form
        public bool IsActive { get; set; }
        public int FailedLoginCount { get; set; }
    }
}
