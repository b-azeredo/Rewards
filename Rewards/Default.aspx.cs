using Rewards.DBModel;
using Rewards.Manager;
using System;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;
using System.EnterpriseServices;
using System.Linq;

namespace Rewards
{
    public partial class _Default : System.Web.UI.Page
    {
        private int USER_ID = 4;

        protected void btnClaim_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Claim")
            {
                int rewardId = Convert.ToInt32(e.CommandArgument);

                if (UserManager.RemoveOneFromStock(rewardId) && UserManager.AddRewardToUser(rewardId, 1))
                {
                    Response.Redirect(Request.RawUrl);
                }
            }
        }

        protected void btnSubmitForm_Click(object sender, EventArgs e)
        {
            int activityId = Convert.ToInt32(activityID.Value);

            using (Entities2 entities = new Entities2())
            {
                var form = new FORM()
                {
                    USER_ID = this.USER_ID,
                    ACTIVITY_ID = activityId,
                    DESCRIPTION = description.Text,
                    STATUS = true,
                    CREATE_DATE = DateTime.Now,
                    MANAGER_DATA_APROVED = DateTime.Now,
                };
                entities.FORM.Add(form);
                entities.SaveChanges();
                Response.Redirect(Request.RawUrl);
            }
        }




        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                /* LEADERBOARD */
                var leaderboardItems = LeaderboardManager.GetLeaderboardItemsFromDatabase();
                lvLeaderboard.DataSource = leaderboardItems;
                lvLeaderboard.DataBind();

                /* REWARDS */
                var RewardsItems = AwardsManager.GetAwardItemsFromDatabase();
                lvRewards.DataSource = RewardsItems;
                lvRewards.DataBind();

                /* ACTIVITIES */
                var activityItems = ActivitiesManager.GetActivityItemsFromDatabase();
                lvActivity.DataSource = activityItems;
                lvActivity.DataBind();

                /* TRANSACTIONS */

                var transactionItems = UserManager.GetTransactionItemsFromDatabase(USER_ID).Take(5).ToList();
                lvTransactions.DataSource = transactionItems;
                lvTransactions.DataBind();

                /* YOUR PROGRESS INFO */
                lifetimePoints.InnerText = $"{UserManager.Get_Lifetime_Points(USER_ID)}";    
                activitiesDone.InnerText = $"{UserManager.Get_Number_Activities_Done(USER_ID)}";
                redeemedRewards.InnerText = $"{UserManager.Get_Number_Redeemed_Rewards(USER_ID)}";

                /* PROFILE INFO */
                profileImage.Src = "data:image;base64," + Convert.ToBase64String(UserManager.Get_Profile_Image(USER_ID));
                profileUsername.InnerHtml = $"{UserManager.Get_Username(USER_ID)}";
                profilePoints.InnerText = $"{UserManager.Get_Current_Points(USER_ID)}";

                /* PROFILE INFO (Modal Transactions) */
                var transactionItems2 = UserManager.GetTransactionItemsFromDatabase(USER_ID).Take(20).ToList();
                lvProfileTransactions.DataSource = transactionItems2;
                lvProfileTransactions.DataBind();

                /* PROFILE INFO (Modal Info) */
                profileImage2.Src = "data:image;base64," + Convert.ToBase64String(UserManager.Get_Profile_Image(USER_ID));
                profileUsername2.InnerHtml = $"{UserManager.Get_Username(USER_ID)}";
                profilePoints2.InnerText = $"{UserManager.Get_Current_Points(USER_ID)}";
                managerEmail.InnerText = $"{UserManager.Get_Manager_Email(USER_ID)}";
            }

        }
    }
}
