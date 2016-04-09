using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Cradle.Models
{
    public class DesignerProfile : Profile
    {
        public string DesignerProfileID { get; set; }
        public string BusinessName { get; set; }
        public string BusinessEmailAddress { get; set; }
        public Statistics ProfileStats { get; set; }
        public List<Collection> Collection { get; set; }

        public virtual Account Account { get; set; }

    }
}