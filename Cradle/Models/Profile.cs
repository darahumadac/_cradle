using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cradle.Models
{
    public abstract class Profile
    {
        public DateTime Birthdate { get; set; } //For designer role, this is date established
        public List<ContactNumber> ContactNumber { get; set; }

    }
}