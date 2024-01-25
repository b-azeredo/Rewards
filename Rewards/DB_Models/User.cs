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
        [MaxLength(100)]
        public string NAME { get; set; }
        [Required]
        [MaxLength(100)]
        public string EMAIL { get; set; }
        [Required]
        [MaxLength(50)]
        public string ROLE { get; set; }
        [Required]
        [MaxLength(100)]
        public string MANAGER_EMAIL { get; set; }
        public byte[] PROFILE_IMAGE { get; set; }   
        [MaxLength(255)]
        public string IMAGE_NAME { get; set; }
        [MaxLength(5)]
        public string IMAGE_EXTENSION { get; set; }
    }
}