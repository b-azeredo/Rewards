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
                    EMAIL = u.EMAIL,
                    MANAGER_EMAIL = u.MANAGER_EMAIL,
                    ROLE = u.ROLE,
                    USERNAME = u.NAME,
                    POINTS = UserManager.Get_Lifetime_Points(u.ID),
                    PROFILE_IMAGE = u.PROFILE_IMAGE,
                    ItemClass = u.ACTIVATED == true ? "" : "deactivatedUser"
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

        public static List<ActivityItem> GetActivityItemsFromDatabase()
        {
            using (Entities2 context = new Entities2())
            {
                List<ACTIVITY> activities = context.ACTIVITY.ToList();

                List<ActivityItem> activitiesItem = activities.Select(a => new ActivityItem
                {
                    ACTIVITY_ID = a.ID,
                    LIMIT_PER_WEEK = a.LIMIT_PER_WEEK,
                    DESCRIPTION = a.DESCRIPTION,
                    NAME = a.NAME,
                    POINTS = a.POINTS,
                    ItemClass = a.ACTIVATED == true ? "activatedActivity" : "deactivatedActivity"
                }).ToList();

                return activitiesItem;
            }
        }
    }
}