using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cradle.Models.Enums;

namespace Cradle.Models
{
    public class Payment
    {
        public PaymentType PaymentOption { get; set; }
        public PaymentDetail PaymentDetails { get; set; }
    }
}
