using Rewards.DBModel;
using Rewards.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using Rewards;
using System.Web.UI.WebControls;
using System.Web.UI;


namespace Rewards.Manager
{
    public class UserManager
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

        public static int Get_Lifetime_Points(int ID)
        {
            using (Entities2 Entities = new Entities2())
            {
                int sum = 0;
                var query = Entities.FORM.Where(p => p.USER_ID == ID && p.STATUS == true).ToList();
                foreach(var Form in query)
                {
                    sum += Form.ACTIVITY.POINTS;
                }

                return sum;
            }
        }

        public static int Get_Current_Points(int ID)
        {
            int lifetimePoints = Get_Lifetime_Points(ID);

            using (Entities2 Entities = new Entities2())
            {
                int sum = 0;
                var query = Entities.PURCHASE.Where(p => p.USER_ID == ID).ToList();
                foreach (PURCHASE Purchase in query)
                {
                    sum += Purchase.REWARD.PRICE;
                }

                return lifetimePoints - sum;
            }
        }

        public static int Get_Number_Activities_Done(int ID)
        {
            using (Entities2 Entities = new Entities2())
            {
                int numActivitiesDone = Entities.FORM.Where(f => f.USER_ID == ID && f.STATUS == true).ToList().Count();
                return numActivitiesDone;
            }
        }

        public static int Get_Number_Redeemed_Rewards(int ID)
        {
            using (Entities2 Entities = new Entities2())
            {
                int numRedeemedRewards = Entities.PURCHASE.Where(p => p.USER_ID == ID).ToList().Count();
                return numRedeemedRewards;
            }
        }

        public static byte[] Get_Profile_Image(int ID)
        {
            using (Entities2 Entities = new Entities2())
            {
                var user = Entities.USER.FirstOrDefault(u => u.ID == ID);
                return user.PROFILE_IMAGE;
            }
        }

        public static string Get_Manager_Email(int ID)
        {
            using (Entities2 Entities = new Entities2())
            {
                var user = Entities.USER.FirstOrDefault(u => u.ID == ID);
                return user.MANAGER_EMAIL;
            }
        }

        public static string Get_Email(int ID)
        {
            using (Entities2 Entities = new Entities2())
            {
                var user = Entities.USER.FirstOrDefault(u => u.ID == ID);
                return user.EMAIL;
            }
        }

        public static string Get_Role(int ID)
        {
            using (Entities2 Entities = new Entities2())
            {
                var user = Entities.USER.FirstOrDefault(u => u.ID == ID);
                return user.ROLE;
            }
        }

        public static bool Get_Activated(int ID)
        {
            using (Entities2 Entities = new Entities2())
            {
                var user = Entities.USER.FirstOrDefault(u => u.ID == ID);
                return user.ACTIVATED;
            }
        }

        public static string Get_Username(int ID)
        {
            using (Entities2 Entities = new Entities2())
            {
                var user = Entities.USER.FirstOrDefault(u => u.ID == ID);
                return user.NAME;
            }
        }

        public static List<TransactionItem> GetTransactionItemsFromDatabase(int ID)
        {
            using (var context = new Entities2())
            {
                List<FORM> forms = context.FORM.ToList();
                List<PURCHASE> purchases = context.PURCHASE.ToList();

                var activitiesList = context.FORM.Where(f => f.USER_ID == ID && f.STATUS == true).ToList();
                var purchasesList = context.PURCHASE.Where(p => p.USER_ID == ID).ToList();

                List<TransactionItem> transactionItems = activitiesList.Select(f => new TransactionItem
                {
                    NAME = $"Activity Done ({f.ACTIVITY.NAME})",
                    DATE = f.CREATE_DATE.ToString(),
                    POINTS = f.ACTIVITY.POINTS,
                    ItemClass = "up"
                }).ToList();

                transactionItems.AddRange(purchasesList.Select(p => new TransactionItem
                {
                    NAME = $"Reward Claimed ({p.REWARD.NAME})",
                    DATE = p.PURCHASE_DATE.ToString(),
                    POINTS = p.REWARD.PRICE * -1,
                    ItemClass = "down"
                }));

                transactionItems = transactionItems.OrderByDescending(t => DateTime.Parse(t.DATE)).ToList();

                foreach (var transactionItem in transactionItems)
                {
                    transactionItem.DATE = DateTime.Parse(transactionItem.DATE).ToString("yyyy/MM/dd");
                }

                return transactionItems;
            }
        }


        public static bool RemoveOneFromStock(int rewardId)
        {
            using (Entities2 entities = new Entities2())
            {
                var stockReward = entities.REWARD_STOCK.FirstOrDefault(sr => sr.REWARD_ID == rewardId);

                if (stockReward != null && stockReward.STOCK > 0)
                {
                    stockReward.STOCK--;
                    entities.SaveChanges();
                    return true;
                }

                return false;
            }
        }


        public static bool AddRewardToUser(int rewardId, int userId)
        {
            using (Entities2 entities = new Entities2())
            {

                int userPoints = Get_Current_Points(userId);
                int rewardPoints = entities.REWARD.Where(r => r.ID == rewardId).Select(r => r.PRICE).FirstOrDefault();

                if (userPoints >= rewardPoints)
                {
                    PURCHASE purchase = new PURCHASE
                    {
                        USER_ID = userId,
                        REWARD_ID = rewardId,
                        PURCHASE_DATE = DateTime.Now
                    };

                    entities.PURCHASE.Add(purchase);
                    entities.SaveChanges();

                    return true;
                }

                return false;
            }
        }

    }
}