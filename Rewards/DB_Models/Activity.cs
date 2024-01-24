using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rewards.DB_Models
{
    public class Activity
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [StringLength(50)]
        public string NAME { get; set; }
        [Required]
        public int POINTS { get; set; }
        public int LIMIT_PER_WEEK { get; set; }
    }
}