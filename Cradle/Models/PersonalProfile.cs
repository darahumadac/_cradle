﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Cradle.Models
{
    public class PersonalProfile : Profile
    {
        [Key, ForeignKey("Account")]
        public string PersonalProfileId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual Account Account { get; set; }
    }
}
