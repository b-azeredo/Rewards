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
        public int IDUSER { get; set; }

        [ForeignKey("Activity")]
        public int IDACTIVITY { get; set; }

        public string DESCRIPTION { get; set; }
        public byte[] FILES { get; set; }
        public string STATUS { get; set; }

        [Column(TypeName = "Date")]
        public DateTime DATE { get; set; }
        public virtual User User { get; set; }
    }
}