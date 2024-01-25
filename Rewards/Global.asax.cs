using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Rewards.DB_Models;
using Rewards.Items;
using Rewards.Manager;
using System.Web.UI.WebControls;


namespace Rewards
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            using (Context context = new Context()) 
            {
                context.Rewards.RemoveRange(context.Rewards);
                context.SaveChanges();
                List<Reward> newReward = new List<Reward>
                {
                    new Reward{NAME = "Air Jordan Blue", PRICE = 300, STOCK = 3, IMAGE_URL = "/Images/Shoes/Air Jordan Blue.png"},
                    new Reward{NAME = "Air Jordan Red", PRICE = 300, STOCK = 3, IMAGE_URL = "/Images/Shoes/Air Jordan Red.png"},
                    new Reward{NAME = "Allstars", PRICE = 150, STOCK = 5, IMAGE_URL = "/Images/Shoes/Allstars.jpg"},
                    new Reward{NAME = "Nike", PRICE = 240, STOCK = 4, IMAGE_URL = "/Images/Shoes/Nike.jpg"},
                    new Reward{NAME = "Addidas", PRICE = 100, STOCK = 6, IMAGE_URL = "/Images/Shoes/Addias.jpg"},
                };
                context.Rewards.AddRange(newReward);
                context.SaveChanges();
            }
        }
    }

}