using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Rewards.DB_Models
{
    public class Purchase
    {
        [Key]
        public int ID { get; set; }
        
        [Column(TypeName = "Date")]
        public DateTime PURCHASE_DATE { get; set; }
        [ForeignKey("User")]
        public int USER_ID { get; set; }
        [ForeignKey("Reward")]
        public int REWARD_ID { get; set; }
        public User User { get; set; }
        public Reward Reward { get; set; }
    }
}