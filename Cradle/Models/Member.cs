using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cradle.Models.Enums;

namespace Cradle.Models
{
    public class Member : RegisteredUser
    {
        public Address DeliveryAddress { get; set; }

        public Member() 
        {
            this.Role = Role.Member;
        }
    }
}