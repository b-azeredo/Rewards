using Rewards.DBModel;
using Rewards.Manager;
using System;
using System.Web.UI;
using System.IO;

namespace Rewards
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                var leaderboardItems = LeaderboardManager.GetLeaderboardItemsFromDatabase();
                lvLeaderboard.DataSource = leaderboardItems;
                lvLeaderboard.DataBind();
                
                /*
                var RewardsItems = AwardsManager.GetAwardItemsFromDatabase();
                lvRewards.DataSource = RewardsItems;
                lvRewards.DataBind();

                */


                /*
                using (Entities2 context = new Entities2())
                {
                    USER user = new USER()
                    {
                        ID = 1,
                        NAME = "Bernardo",
                        EMAIL = "email",
                        ROLE = "ADMIN",
                        MANAGER_EMAIL = "email",
                        PROFILE_IMAGE = File.ReadAllBytes("C:\\Users\\Bernardo Azeredo\\source\\repos\\b-azeredo\\Rewards\\Rewards\\icon\\user-solid.svg"),
                        IMAGE_NAME = "image",
                        IMAGE_EXTENSION = "svg",
                    };
                    context.USER.Add(user);
                    context.SaveChanges();
                }
                */

                var activityItems = ActivitiesManager.GetActivityItemsFromDatabase();
                lvActivity.DataSource = activityItems;
                lvActivity.DataBind();

                lifetimePoints.InnerText = $"{UserManager.Get_Lifetime_Points(1)}";            }
        }
    }
}
