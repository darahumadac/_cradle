using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Cradle.Models
{
    public abstract class Profile
    {
        //Member:   This is the deliveryAddress
        //Designer: This is the businessAddress
        public Address Address { get; set; }
        public DateTime Birthdate { get; set; } //For designer role, this is date established
        public List<ContactNumber> ContactNumber { get; set; }

    }
}