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
                /* LEADERBOARD */
                var leaderboardItems = LeaderboardManager.GetLeaderboardItemsFromDatabase();
                lvLeaderboard.DataSource = leaderboardItems;
                lvLeaderboard.DataBind();

                /*
                var RewardsItems = AwardsManager.GetAwardItemsFromDatabase();
                lvRewards.DataSource = RewardsItems;
                lvRewards.DataBind();
                */

                /* ACTIVITIES */
                var activityItems = ActivitiesManager.GetActivityItemsFromDatabase();
                lvActivity.DataSource = activityItems;
                lvActivity.DataBind();

                /* YOUR PROGRESS INFO */
                lifetimePoints.InnerText = $"{UserManager.Get_Lifetime_Points(4)}";    
                activitiesDone.InnerText = $"{UserManager.Get_Number_Activities_Done(4)}";
                redeemedRewards.InnerText = $"{UserManager.Get_Number_Redeemed_Rewards(4)}";

                /* PROFILE INFO */
                profileImage.Src = "data:image;base64," + Convert.ToBase64String(UserManager.Get_Profile_Image(4));
                profileUsername.InnerHtml = $"{UserManager.Get_Username(4)}" + "<br /> <span class=\"black\">Online</span>";
                profilePoints.InnerText = $"{UserManager.Get_Current_Points(4)}";

            }
        }
    }
}
