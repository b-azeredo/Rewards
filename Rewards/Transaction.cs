using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Rewards
{
    public class Transaction
    {
        public int ID { get; set; }

        [ForeignKey("User")]
        public int IDUSER { get; set; }
        public int VALUE;

        [Column(TypeName = "Date")]
        public DateTime DATE { get; set; }

        public string DESCRIPTION { get; set; }
    }
}