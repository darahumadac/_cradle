using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Cradle.Models
{
    public class PersonalProfile : Profile
    {
        [Key, ForeignKey("User")]
        public string RegisteredUserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public virtual RegisteredUser User { get; set; }
    }
}
