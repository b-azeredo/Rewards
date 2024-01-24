using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rewards
{
    public class Form
    {
        public int ID { get; set; }

        [ForeignKey("User")]
        public int ID_USER { get; set; }

        [ForeignKey("Activity")]
        public int ID_ACTIVITY { get; set; }

        public string DESCRIPTION { get; set; }
        public byte[] FILES { get; set; }
        public string STATUS { get; set; }

        [Column(TypeName = "Date")]
        public DateTime DATE { get; set; }
        public virtual User User { get; set; }
    }
}