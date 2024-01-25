using Rewards.DB_Models;
using Rewards.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rewards.Manager
{
    public class AwardsManager
    {
        public static List<AwardsItem> GetAwardItemsFromDatabase()
        {
            using (Context context = new Context())
            {
                List<Reward> rewards = context.Rewards.OrderByDescending(r => r.PRICE).ToList();

                List<AwardsItem> awardsItems = rewards.Select(r => new AwardsItem
                {
                    NAME = r.NAME,
                    PRICE = r.PRICE
                }).ToList();

                return awardsItems;
            }
        }
    }
}