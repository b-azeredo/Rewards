using Rewards.DBModel;
using Rewards.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rewards.Manager
{
    public class LeaderboardManager
    {

        public static List<LeaderboardItem> GetLeaderboardItemsFromDatabase()
        {
            using (var context = new Entities2())
            {
                List<USER> users = context.USER.ToList();

                List<LeaderboardItem> leaderboardItems = users.Select(u => new LeaderboardItem
                {
                    USERNAME = u.NAME,
                    POINTS = UserManager.Get_Lifetime_Points(u.ID),
                    PROFILE_IMAGE = u.PROFILE_IMAGE
                }).ToList();

                leaderboardItems.OrderByDescending(u => u.POINTS).ToList();
                return leaderboardItems;
            }
        }
    }
}