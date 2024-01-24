using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Rewards
{
    public class Purchase
    {
        public int ID { get; set; }

        [ForeignKey("User")]
        public int ID_USER { get; set; }

        [ForeignKey("Reward")]
        public int ID_REWARD { get; set; }

        [Column(TypeName = "Date")]
        public DateTime DATE { get; set; }
    }
}