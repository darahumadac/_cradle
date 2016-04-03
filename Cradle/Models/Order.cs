using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cradle.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public int OrderItem { get; set; }
        public int Quantity { get; set; }
        public Payment PaymentDetails { get; set; }
        public Delivery DeliveryDetails { get; set; }
    }
}