using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cradle.Models
{
    public class OrderItem
    {
        public int OrderItemID { get; set; }
        public Item ItemOrdered { get; set; }
        public int Quantity { get; set; }

    }
}
