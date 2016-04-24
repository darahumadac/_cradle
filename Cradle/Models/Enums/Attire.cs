using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Cradle.Models.Enums
{
    public enum Attire
    {
        [Description("Business Casual")]
        Business_Casual = 0,
        Casual = 1,
        Formal = 2,
        Party = 3,
        [Description("Semi Formal")]
        Semi_Formal = 4,
        [Description("Smart Casual")]
        Smart_Casual = 5
    }
}