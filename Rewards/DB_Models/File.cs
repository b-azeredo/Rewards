using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rewards.DB_Models
{
    public class File
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("Forms")]
        public int FORM_ID { get; set; }
        public byte[] CONTENT { get; set; }
        [MaxLength(255)]
        public string NAME { get; set; }
        [MaxLength(5)]
        public string EXTENSION {  get; set; }

        public Forms Forms { get; set; }
    }
}