using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Rewards.DB_Models
{
    public class Reward
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(50)]
        public string NAME { get; set; }
        [Required]
        public int PRICE { get; set; }
        public int STOCK { get; set; }
        public byte[] IMAGE { get; set; }
    }
}