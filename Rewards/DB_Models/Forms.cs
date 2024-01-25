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
        public int USER_ID { get; set; }

        [ForeignKey("Activity")]
        public int ACTIVITY_ID { get; set; }
        [MaxLength(8000)]
        public string DESCRIPTION { get; set; }
        public bool STATUS { get; set; }

        [Column(TypeName = "Date")]
        public DateTime CREATE_DATE { get; set; }


        [Column(TypeName = "Date")]
        public DateTime MANAGER_DATA_APROVED { get; set; }
        public User User { get; set; }
        public Activity Activity { get; set; }
    }
}