using Rewards.DBModel;
using Rewards.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

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
                    ACTIVITY_ID = a.ID,
                    DESCRIPTION = a.DESCRIPTION,
                    NAME = a.NAME,
                    POINTS = a.POINTS
                }).ToList();

                return activitiesItem;
            }
        }
        public static string Get_Name(int activityID)
        {
            using (var entities = new Entities2())
            {
                var activity = entities.ACTIVITY.FirstOrDefault(u => u.ID == activityID);
                return activity.NAME;
            }
        }
        public static string Get_Description(int activityID)
        {
            using (var entities = new Entities2())
            {
                var activity = entities.ACTIVITY.FirstOrDefault(u => u.ID == activityID);
                return activity.DESCRIPTION;
            }
        }


        public static int Get_Points(int activityID)
        {
            using (var entities = new Entities2())
            {
                var activity = entities.ACTIVITY.FirstOrDefault(u => u.ID == activityID);
                return activity.POINTS;
            }
        }

        public static bool Get_Activated(int activityID)
        {
            using (var entities = new Entities2())
            {
                var activity = entities.ACTIVITY.FirstOrDefault(u => u.ID == activityID);
                return activity.ACTIVATED;
            }
        }
    }
}