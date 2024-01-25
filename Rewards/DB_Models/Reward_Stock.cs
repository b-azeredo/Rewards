using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rewards.DB_Models
{
    public class Reward_Stock
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("Reward")]
        public int REWARD_ID { get; set; }
        [Required]
        public int STOCK {  get; set; }
        public Reward Reward { get; set; }
    }
}