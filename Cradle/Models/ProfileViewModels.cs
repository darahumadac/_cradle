using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cradle.Models
{
    public class DesignerProfileViewModel
    {
        public string DesignerName { get; set; }
        public string Tagline { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
        public List<string> MadeType { get; set; }
        public string Specialization { get; set; }
        public Dictionary<string, string> DeliveryTime { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public List<Collection> Collections { get; set; }


        
    }
}