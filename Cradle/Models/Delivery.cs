using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cradle.Models.Enums;

namespace Cradle.Models
{
    public class Delivery
    {
        public DeliveryOptions DeliveryOptions { get; set; }
        public Address DeliveryAddress { get; set; }

    }
}
