using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cradle.Models.Enums;

namespace Cradle.Models
{
    public class Delivery
    {
        public int DeliveryID { get; set; }
        public DeliveryOptions DeliveryOptions { get; set; }
        public Address DeliveryAddress { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }

    }
}
