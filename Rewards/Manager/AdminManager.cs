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
        public static List<RewardsItem> GetRewardsFromDatabase()
        {
            using (var context = new Entities2())
            {
                var rewardsItems = context.REWARD
                    .Select(r => new RewardsItem
                    {
                        ID = r.ID,
                        NAME = r.NAME,
                        PRICE = r.PRICE,
                        IMAGE = r.IMAGE,
                        ACTIVATED = r.ACTIVATED,
                        ItemClass = r.REWARD_STOCK.Any(rs => rs.STOCK > 0) && r.ACTIVATED ? "inStock" : "outOfStock"
                    })
                    .OrderBy(r => r.ItemClass)
                    .ThenByDescending(r => r.PRICE)
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
                    DESCRIPTION = a.DESCRIPTION,
                    NAME = a.NAME,
                    POINTS = a.POINTS,
                    ItemClass = a.ACTIVATED == true ? "activatedActivity" : "deactivatedActivity"
                })
                .OrderBy(r => r.ItemClass)
                .ThenByDescending(r => r.POINTS)
                .ToList();



                return activitiesItem;
            }
        }


    }
}