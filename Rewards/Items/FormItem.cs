using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rewards.Item
{
    public class FormItem
    {
        public int ID { get; set; }
        public int USER_ID { get; set; }
        public int ACTIVITY_ID { get; set; }
        public string DESCRIPTION { get; set; }
        public bool? STATUS { get; set; }
        public DateTime CREATE_DATE { get; set; }
        public DateTime? MANAGER_DATA_APROVED { get; set; }
    }
}