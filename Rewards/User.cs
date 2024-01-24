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
        public string EMAIL_MANAGER { get; set; }
        public int CLAIMED_AWARDS { get; set; }
        public byte[] PROFILEIMAGE { get; set; }
    }
}