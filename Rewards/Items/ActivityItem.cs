using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rewards.Items
{
    public class ActivityItem
    {
        public int ACTIVITY_ID { get; set; }
        public string NAME { get; set; }
        public int POINTS { get; set; }
        public string DESCRIPTION { get; set; }
        public int LIMIT_PER_WEEK { get; set; }
        public string ItemClass { get; set; }
    }
}