using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cradle.Models.Enums;

namespace Cradle.Models
{
    public class Designer : RegisteredUser
    {
        public DesignerProfile BusinessProfile { get; set; }
        public Collection Collection { get; set; }

        public Designer()
        {
            this.Role = Role.Designer;
        }
    }
}