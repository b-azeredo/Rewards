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

                var activityItems = ActivitiesManager.GetActivityItemsFromDatabase();
                lvActivity.DataSource = activityItems;
                lvActivity.DataBind();

                lifetimePoints.InnerText = $"{UserManager.Get_Lifetime_Points(4)}";    
                activitiesDone.InnerText = $"{UserManager.Get_Number_Activities_Done(4)}";
                redeemedRewards.InnerText = $"{UserManager.Get_Number_Redeemed_Rewards(4)}";
            }
        }
    }
}
