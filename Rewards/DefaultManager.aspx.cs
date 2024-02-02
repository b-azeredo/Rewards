using Rewards.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Rewards
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        private int USER_ID = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            /* LEADERBOARD */
            var leaderboardItems = LeaderboardManager.GetLeaderboardItemsFromDatabase();
            lvLeaderboard.DataSource = leaderboardItems;
            lvLeaderboard.DataBind();

            /* REWARDS */
            var RewardsItems = RewardsManager.GetRewardItemsFromDatabase();
            lvRewards.DataSource = RewardsItems;
            lvRewards.DataBind();

            /* ACTIVITIES */
            var activityItems = ActivitiesManager.GetActivityItemsFromDatabase();
            lvActivity.DataSource = activityItems;
            lvActivity.DataBind();


            /* YOUR PROGRESS INFO */
            lifetimePoints.InnerText = $"{UserManager.Get_Lifetime_Points(USER_ID)}";
            activitiesDone.InnerText = $"{UserManager.Get_Number_Activities_Done(USER_ID)}";
            redeemedRewards.InnerText = $"{UserManager.Get_Number_Redeemed_Rewards(USER_ID)}";

            /* PROFILE INFO */
            profileImage.Src = "data:image;base64," + Convert.ToBase64String(UserManager.Get_Profile_Image(USER_ID));
            profileUsername.InnerHtml = $"{UserManager.Get_Username(USER_ID)}";
            profilePoints.InnerText = $"{UserManager.Get_Current_Points(USER_ID)}";

            /* PROFILE INFO (Modal Info) */
            profileImage2.Src = "data:image;base64," + Convert.ToBase64String(UserManager.Get_Profile_Image(USER_ID));
            profileUsername2.InnerHtml = $"{UserManager.Get_Username(USER_ID)}";
            profilePoints2.InnerText = $"{UserManager.Get_Current_Points(USER_ID)}";
            managerEmail.InnerText = $"{UserManager.Get_Manager_Email(USER_ID)}";
        }

        protected void btnSaveProfileChanges_Click(object sender, EventArgs e)
        {
            
        }
    }
}
