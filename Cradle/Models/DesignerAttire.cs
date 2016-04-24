using Cradle.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cradle.Models
{
    public class DesignerAttire
    {
        public int DesignerAttireID { get; set; }
        public Attire Attire { get; set; }
        public bool IsSelected { get; set; }
        public virtual DesignerProfile DesignerProfile { get; set; }
    }
}