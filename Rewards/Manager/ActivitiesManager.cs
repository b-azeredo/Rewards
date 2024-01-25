using Rewards.DB_Models;
using Rewards.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rewards.Manager
{
    public class ActivitiesManager
    {
        public static List<ActivityItem> GetActivityItemFromDatabase()
        {
            using (Context context = new Context())
            {
                List<Activity> activities = context.Activities.ToList();

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