using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cradle.Models.Enums;

namespace Cradle.Models
{
    public class ProductInformation
    {
        public int ProductInformationID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public List<Gender> ForGenders { get; set; }
        public List<Attire> ForAttires { get; set; }
        public Statistics ProductStatistics{ get; set; }
    }
}
