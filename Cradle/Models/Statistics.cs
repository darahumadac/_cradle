using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cradle.Models
{
    public class Statistics
    {
        public string StatisticsID { get; set; }
        public int LikeCount { get; set; }
        public int ViewCount { get; set; }
        public int TagCount { get; set; } //follower count
        public double AveRating { get; set; }
    }
}
