using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cradle.Models
{
    public class DesignerProfile : Profile
    {
        public string BusinessName { get; set; }
        public Address BusinessAddress { get; set; }
        public string BusinessEmailAddress { get; set; }
        public Statistics ProfileStats { get; set; }
    }
}