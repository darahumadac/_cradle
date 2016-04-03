using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cradle.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cradle.Models
{
    public class Designer : RegisteredUser
    {
        public DesignerProfile DesignerProfile { get; set; }
        //public List<Collection> Collection { get; set; }
    }
}