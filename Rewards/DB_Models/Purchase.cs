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
        public DateTime DATE { get; set; }
        [ForeignKey("User")]
        public int ID_USER { get; set; }
        [ForeignKey("Reward")]
        public int ID_REWARD { get; set; }
        public User User { get; set; }
        public Reward Reward { get; set; }
    }
}