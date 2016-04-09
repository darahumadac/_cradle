using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cradle.Models.Enums;

namespace Cradle.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public Payment PaymentDetails { get; set; }
        public Delivery DeliveryDetails { get; set; }

        public virtual Account Account { get; set; }
    }
}