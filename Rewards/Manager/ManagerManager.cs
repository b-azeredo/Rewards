using Rewards.DBModel;
using Rewards.Item;
using Rewards.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
 
namespace Rewards.Manager
{
    public class ManagerManager
    {
        public static List<FormItem> GetRequestedActivitiesItemsFromDatabase()
        {
            using (Entities2 context = new Entities2())
            {
                List<FORM> forms = (
                    from FORM in context.FORM
                    join USER in context.USER on FORM.USER_ID equals USER.ID
                    where USER.ROLE == "Employee" && context.USER.Any(manager => manager.ROLE == "Manager" && manager.EMAIL == USER.MANAGER_EMAIL)
                    select FORM
                ).ToList();

                List<FormItem> FormsItems = forms.Select(a => new FormItem
                {
                    ID = a.ID,
                    USER_ID = a.USER_ID,
                    ACTIVITY_ID = a.ACTIVITY_ID,
                    DESCRIPTION = a.DESCRIPTION,
                    STATUS = a.STATUS,
                    CREATE_DATE = a.CREATE_DATE,
                    MANAGER_DATA_APROVED = a.MANAGER_DATA_APROVED
                }).ToList();

                return FormsItems;
            }
        }
        public static List<LeaderboardItem> GetTeamLeaderboardItemsFromDatabase()
        {
            using (var context = new Entities2())
            {
                List<USER> users = (
                    from u in context.USER
                    where u.ROLE == "EMPLOYEE" && u.ACTIVATED == true &&
                    context.USER.Any(manager => manager.ROLE == "Manager" && manager.EMAIL == u.MANAGER_EMAIL)
                    select u
                ).ToList();

                List<LeaderboardItem> leaderboardItems = users.Select(u => new LeaderboardItem
                {
                    USERNAME = u.NAME,
                    POINTS = UserManager.Get_Lifetime_Points(u.ID),
                    PROFILE_IMAGE = u.PROFILE_IMAGE
                }).ToList();

                leaderboardItems = leaderboardItems.OrderByDescending(u => u.POINTS).Take(15).ToList();
                return leaderboardItems;
            }
        }

        public static int Get_Team_Lifetime_Points(int ID)
        {
            using (var context = new Entities2())
            {
                int sum = 0;

                List<USER> users = (
                    from u in context.USER
                    where u.ROLE == "EMPLOYEE" && u.ACTIVATED == true &&
                    context.USER.Any(manager => manager.ROLE == "Manager" && manager.EMAIL == u.MANAGER_EMAIL)
                    select u
                ).ToList();

                foreach (var user in users)
                {
                    sum += context.FORM
                        .Where(p => p.USER_ID == user.ID && p.STATUS == true)
                        .Sum(form => form.ACTIVITY.POINTS);
                }

                return sum;
            }
        }
        public static int Get_Team_Number_Activities_Done(int managerID)
        {
            using (var context = new Entities2())
            {
                List<USER> users = (
                    from u in context.USER
                    where u.ROLE == "EMPLOYEE" && u.ACTIVATED == true &&
                    context.USER.Any(manager => manager.ROLE == "Manager" && manager.EMAIL == u.MANAGER_EMAIL)
                    select u
                ).ToList();

                int numActivitiesDone = 0;

                foreach (var user in users)
                {
                    numActivitiesDone += context.FORM
                        .Count(f => f.USER_ID == user.ID && f.STATUS == true);
                }

                return numActivitiesDone;
            }
        }

        public static int Get_Team_Number_Redeemed_Rewards(int managerID)
        {
            using (var context = new Entities2())
            {
                List<USER> users = (
                    from u in context.USER
                    where u.ROLE == "EMPLOYEE" && u.ACTIVATED == true &&
                    context.USER.Any(manager => manager.ROLE == "Manager" && manager.EMAIL == u.MANAGER_EMAIL)
                    select u
                ).ToList();

                int numRedeemedRewards = 0;

                foreach (var user in users)
                {
                    numRedeemedRewards += context.PURCHASE
                        .Count(p => p.USER_ID == user.ID);
                }

                return numRedeemedRewards;
            }
        }

    }
}