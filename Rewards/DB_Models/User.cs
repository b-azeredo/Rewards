using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;


namespace Rewards.DB_Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(50)]
        public string NAME { get; set; }
        [Required]
        [MaxLength(50)]
        public string EMAIL { get; set; }
        [Required]
        [MaxLength(200)]
        public string PASSWORD { get; set; }
        public int POINTS { get; set; }
        [Required]
        [MaxLength(50)]
        public string ROLE { get; set; }
        [Required]
        [MaxLength(50)]
        public string EMAIL_MANAGER { get; set; }
        public byte[] PROFILE_IMAGE { get; set; }        
    }
}