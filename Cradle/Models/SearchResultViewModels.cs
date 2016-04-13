using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cradle.Models
{
    public class DesignerResultsViewModel
    {
        public DesignerResultsViewModel(DesignerProfile designer)
        {
            DesignerName = designer.BusinessName;
        }

        public string DesignerName { get; set; }
    }
}