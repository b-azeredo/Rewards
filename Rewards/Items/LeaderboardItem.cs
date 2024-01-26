using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rewards.Items
{
    public class LeaderboardItem
    {
        public byte[] PROFILE_IMAGE { get; set; }
        public string USERNAME { get; set; }
        public int POINTS { get; set; }
    }
}