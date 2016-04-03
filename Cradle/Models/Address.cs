using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cradle.Models
{
    public class Address
    {
        public string AddressID { get; set; }
        public string StreetNo { get; set; }
        public string StreetName { get; set; }
        public string Municipality { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
    }
}
