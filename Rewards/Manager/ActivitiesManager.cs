using Rewards.DBModel;
using Rewards.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rewards.Manager
{
    public class ActivitiesManager
    {
        public static List<ActivityItem> GetActivityItemsFromDatabase()
        {
            using (Entities2 context = new Entities2())
            {
                List<ACTIVITY> activities = context.ACTIVITY.Where(a => a.ACTIVATED == true).ToList();

                List<ActivityItem> activitiesItem = activities.Select(a => new ActivityItem
                {
                    NAME = a.NAME,
                    POINTS = a.POINTS
                }).ToList();

                return activitiesItem;
            }
        }
    }
}