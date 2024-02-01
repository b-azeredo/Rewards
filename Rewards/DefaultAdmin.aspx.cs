using Rewards.DBModel;
using Rewards.Manager;
using System;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;
using System.EnterpriseServices;
using System.Linq;
using System.Web;

namespace Rewards
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void btnEditUser_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnComfirmUserChanges_Click(object sender, EventArgs e)
        {
            int userID;
            
        }


        protected void btnComfirmAddActivity_Click(object sender, EventArgs e)
        {
            using (Entities2 entities2 = new Entities2())
            {
                ACTIVITY newActivity = new ACTIVITY()
                {
                    NAME = ActivityName.Text,
                    DESCRIPTION = ActivityDescription.Text,
                    ACTIVATED = true,
                    POINTS = int.Parse(ActivityPoints.Text),
                    LIMIT_PER_WEEK = int.Parse(ActivityLimit.Text),
                };
                entities2.ACTIVITY.Add(newActivity);
                entities2.SaveChanges();
                Response.Redirect(Request.RawUrl);
            }
        }

        protected void btnComfirmAddUser_Click(object sender, EventArgs e)
        {
            string role = dlRoleUser.SelectedValue;
            using (Entities2 entities2 = new Entities2())
            {
                USER newuser = new USER
                {
                    NAME = UserName.Text,
                    EMAIL = UserEmail.Text,
                    ROLE = role,
                    PROFILE_IMAGE = FileUpload1.FileBytes,
                    MANAGER_EMAIL = (role == "EMPLOYEE") ? managerEmailTextBox.Text : null,
                    ACTIVATED = true
                };
                entities2.USER.Add(newuser);
                entities2.SaveChanges();
                Response.Redirect(Request.RawUrl);
            }
        }

        protected void btnComfirmAddReward_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txbRewardPrice.Text, out _))
            {
                string script = "alert('Por favor, insira um valor numérico válido para o preço.');";
                ClientScript.RegisterStartupScript(this.GetType(), "ValidationAlert", script, true);
                return;
            }

            if (RewardImage.HasFile)
            {
                string fileExtension = System.IO.Path.GetExtension(RewardImage.FileName).ToLower();
                if (fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png")
                {
                    using (Entities2 entities2 = new Entities2())
                    {
                        REWARD newReward = new REWARD
                        {
                            NAME = txbRewardName.Text,
                            PRICE = int.Parse(txbRewardPrice.Text),
                            IMAGE = RewardImage.FileBytes,
                            ACTIVATED = true
                        };
                        entities2.REWARD.Add(newReward);
                        entities2.SaveChanges();
                        Response.Redirect(Request.RawUrl);

                    };
                }
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
                var RewardsItems = AdminManager.GetRewardsFromDatabase();
                lvRewards.DataSource = RewardsItems;
                lvRewards.DataBind();

                /* ACTIVITIES */
                var activityItems = AdminManager.GetActivityItemsFromDatabase();
                lvActivity.DataSource = activityItems;
                lvActivity.DataBind();

                /* USERS */
                var userItems = AdminManager.GetUsersFromDatabase();
                lvUsers.DataSource = userItems;
                lvUsers.DataBind();

            }
        }


    }
}