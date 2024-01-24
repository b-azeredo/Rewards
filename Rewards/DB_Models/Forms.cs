using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Rewards.DB_Models
{
    public class Forms
    {
        [Key]
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
        public User User { get; set; }
        public Activity Activity { get; set; }
    }
}