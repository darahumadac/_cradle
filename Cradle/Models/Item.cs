using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cradle.Models.Enums;

namespace Cradle.Models
{
    public class Item
    {
        public int ItemID { get; set; }
        public ProductInformation ItemInfo { get; set; }
        public ItemType ItemType { get; set; }
        public string ItemSubtype { get; set; }
        public string Material { get; set; }
        public decimal RegularPrice { get; set; }
        public decimal DiscountedPrice { get; set; }
        public virtual Image ItemImage { get; set; }

    }
}