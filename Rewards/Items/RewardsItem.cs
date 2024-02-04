using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rewards.Items
{
    public class RewardsItem
    {
        public int ID {  get; set; }
        public string NAME { get; set; }
        public int PRICE { get; set; }
        public byte[] IMAGE { get; set; }
        public bool ACTIVATED { get; set; }
        public string ItemClass { get; set; }
    }
}