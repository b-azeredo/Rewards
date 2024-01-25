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
                var query = Entities.FORM.Where(p => p.ID == ID && p.STATUS.Equals(true)).ToList();
                foreach(FORM Form in query)
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
    }
}