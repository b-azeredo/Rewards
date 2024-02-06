using Rewards.DBModel;
using Rewards.Items;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Linq;
using System.Web;

namespace Rewards.Manager
{
    public class RewardsManager
    {
        public static int GetRewardStock(int rewardID)
        {
            using (Entities2 entities = new Entities2())
            {
                int stock = entities.REWARD_STOCK
                                    .Where(r => r.REWARD_ID == rewardID)
                                    .Select(r => r.STOCK)
                                    .DefaultIfEmpty(0)
                                    .Sum();
                return stock;
            }
        }

        public static void ClearRewardStock(int rewardID)
        {
            using (Entities2 entities = new Entities2())
            {
                int stock = GetRewardStock(rewardID);
                var rewardStock = new REWARD_STOCK()
                {
                    REWARD_ID = rewardID,
                    STOCK = stock * -1
                };
                entities.REWARD_STOCK.Add(rewardStock);
                entities.SaveChanges();
            }
        }
        public static void ChangeStatus(int rewardID)
        {
            using (Entities2 entities = new Entities2())
            {
                var reward = entities.REWARD.FirstOrDefault(r => r.ID == rewardID);
                if (reward.ACTIVATED == true)
                {
                    reward.ACTIVATED = false;
                }
                else
                {
                    reward.ACTIVATED = true;
                }
                entities.SaveChanges();
            }
        }


        public static List<RewardsItem> GetRewardItemsFromDatabase()
        {
            using (var context = new Entities2())
            {
                List<REWARD> rewards = context.REWARD
                    .Where(r => r.REWARD_STOCK.Any(rs => rs.STOCK > 0) && r.ACTIVATED == true)
                    .OrderByDescending(r => r.PRICE)
                    .ToList();


                List<RewardsItem> rewardsItems = rewards.Select(r => new RewardsItem
                {
                    ID = r.ID,
                    NAME = r.NAME,
                    PRICE = r.PRICE,
                    IMAGE = r.IMAGE,
                }).ToList();

                return rewardsItems;
            }
        }
    }
}