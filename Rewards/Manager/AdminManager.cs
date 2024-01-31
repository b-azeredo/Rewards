using Rewards.DBModel;
using Rewards.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rewards.Manager
{
    public class AdminManager
    {
        public static List<LeaderboardItem> GetUsersFromDatabase()
        {
            using (var context = new Entities2())
            {
                List<USER> users = context.USER.ToList();

                List<LeaderboardItem> leaderboardItems = users.Select(u => new LeaderboardItem
                {
                    ID = u.ID,
                    USERNAME = u.NAME,
                    POINTS = UserManager.Get_Lifetime_Points(u.ID),
                    PROFILE_IMAGE = u.PROFILE_IMAGE
                }).ToList();

                return leaderboardItems;
            }
        }

        public static List<RewardsItem> GetRewardsFromDatabase()
        {
            using (var context = new Entities2())
            {
                var rewardsItems = context.REWARD
                    .OrderByDescending(r => r.PRICE)
                    .Select(r => new RewardsItem
                    {
                        ID = r.ID,
                        NAME = r.NAME,
                        PRICE = r.PRICE,
                        IMAGE = r.IMAGE,
                        ItemClass = r.REWARD_STOCK.Any(rs => rs.STOCK > 0) && r.ACTIVATED == true ? "inStock" : "outOfStock"
                    })
                    .ToList();

                return rewardsItems;
            }
        }
    }
}