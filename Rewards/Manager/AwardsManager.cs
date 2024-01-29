using Rewards.DBModel;
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
            using (var context = new Entities2())
            {
                List<REWARD> rewards = context.REWARD.OrderByDescending(r => r.PRICE).ToList();

                List<AwardsItem> awardsItems = rewards.Select(r => new AwardsItem
                {
                    NAME = r.NAME,
                    PRICE = r.PRICE,
                    IMAGE = r.IMAGE,
                }).ToList();

                return awardsItems;
            }
        }
    }
}