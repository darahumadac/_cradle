using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cradle.Models
{
    public class Measurement
    {
        public int Height { get; set; }
        public int ArmLength { get; set; }
        public int ArmWidth { get; set; }
        public int Waistline { get; set; }
        public int Bust { get; set; }
        public int LegLength { get; set; }
        public int LegWidth { get; set; }
    }
}