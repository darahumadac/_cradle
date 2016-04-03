using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cradle.Models.Enums;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Cradle.Models
{
    public class RegisteredUser
    {
        [ForeignKey("Account")]
        public string RegisteredUserID { get; set; }
        public virtual Account Account { get; set; }
        public virtual PersonalProfile PersonalProfile { get; set; }

        //Member:   This is the deliveryAddress
        //Designer: This is the businessAddress
        public virtual Address PrimaryAddress { get; set; }
        public virtual List<Order> Orders { get; set; }
        
    }
}