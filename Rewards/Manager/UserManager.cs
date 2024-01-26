using Rewards.DBModel;
using Rewards.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;


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

        public static string Get_Profile_Image(int ID)
        {
            
        }
    }
}