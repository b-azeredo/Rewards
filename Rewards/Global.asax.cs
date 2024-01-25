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
                Activity reward = new Activity()
                {
                    NAME = "name",
                    POINTS = 192,
                };
                context.Activities.Add(reward);
                context.SaveChanges();
            }
        }
    }

}