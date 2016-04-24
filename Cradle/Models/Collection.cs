using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cradle.Models
{
    public class Collection
    {
        public int CollectionID { get; set; }
        public ProductInformation CollectionInfo { get; set; }
        public string ThemeKeywords { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice{ get; set; }
        public List<Item> CollectionItems { get; set; }

        public virtual DesignerProfile DesignerProfile { get; set; }

    }
}