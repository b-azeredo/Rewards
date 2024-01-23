using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rewards
{
    public class Reward
    {
        public int ID {  get; set; }
        public string NAME { get; set; }
        public int PRICE { get; set; }
        public int STOCK { get; set; }
        public byte[] IMAGE { get; set; }
    }
}