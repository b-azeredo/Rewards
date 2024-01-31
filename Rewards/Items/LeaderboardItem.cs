using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rewards.Items
{
    public class LeaderboardItem
    {
        public int ID { get; set; }
        public byte[] PROFILE_IMAGE { get; set; }
        public string USERNAME { get; set; }
        public string EMAIL { get; set; }
        public string ROLE {  get; set; }
        public string MANAGER_EMAIL { get; set; }
        public int POINTS { get; set; }
    }
}