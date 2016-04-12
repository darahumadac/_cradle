using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cradle.Models
{
    public class ContactNumber
    {
        public int ContactNumberID { get; set; }
        public string LandlineNo { get; set; }
        public string CountryCodeMobile { get; set; }
        public string MobileNo { get; set; }
        public virtual PersonalProfile PersonalProfile { get; set; }
        public virtual DesignerProfile DesignerProfile { get; set; }
    }
}
