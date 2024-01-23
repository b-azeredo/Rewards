using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rewards
{
    public class Activity
    {
        public int ID { get; set; }
        public string NAME { get; set; }
        public int POINTS { get; set; }
        public int LIMITPERWEEK { get; set; }
    }
}