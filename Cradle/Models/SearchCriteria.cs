using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cradle.Models
{
    public class SearchCriteria
    {
        public SearchCriteria() { }
        public SearchCriteria(string designerType)
        {
            DesignerType = designerType;
        }

        public string DesignerType { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string BodyBuild { get; set; }
        public decimal Budget { get; set; }
        public Measurement BodyMeasurements { get; set; }
    }
}