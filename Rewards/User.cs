using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rewards
{
    public class User
    {
        public int ID { get; set; }
        public string NAME { get; set; }
        public string PASSWORD { get; set; }
        public int POINTS { get; set; }
        public string ROLE { get; set; }
        public string EMAILBOSS { get; set; }
        public int CLAIMEDAWARDS { get; set; }
        public int LIFETIMEPOINTS { get; set; }
        public byte[] PROFILEIMAGE { get; set; }
    }
}