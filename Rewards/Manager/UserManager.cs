using Rewards.DBModel;
using Rewards.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using Rewards;


namespace Rewards.Manager
{
    public class UserManager
    {
        
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

                transactionItems = transactionItems.OrderByDescending(t => t.DATE).ToList();

                foreach (var transactionItem in transactionItems)
                {
                    transactionItem.DATE = DateTime.Parse(transactionItem.DATE).ToString("yyyy/MM/dd");
                }

                transactionItems = transactionItems.Take(5).ToList();

                return transactionItems;
            }
        }

    }
}