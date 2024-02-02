using Rewards.DBModel;
using Rewards.Manager;
using System;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using Rewards.Items;

namespace Rewards
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void btnEditUser_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnComfirmUserChanges_Click(object sender, EventArgs e)
        {

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

        /* DATA BOUNDS */

        protected void lvActivity_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                var item = e.Item.DataItem as ActivityItem;
                var activityNameLiteral = e.Item.FindControl("activityNameLiteral") as Literal;
                var pointsLiteral = e.Item.FindControl("pointsLiteral") as Literal;

                activityNameLiteral.Text = item.NAME;
                pointsLiteral.Text = item.POINTS.ToString();
            }
        }


        protected void lvRewards_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                var item = e.Item.DataItem as RewardsItem;
                var rewardImage = e.Item.FindControl("rewardImage") as Image;
                var rewardNameLiteral = e.Item.FindControl("rewardNameLiteral") as Literal;
                var rewardPriceLiteral = e.Item.FindControl("rewardPriceLiteral") as Literal;

                rewardImage.ImageUrl = $"data:image;base64,{Convert.ToBase64String(item.IMAGE)}";
                rewardNameLiteral.Text = item.NAME;
                rewardPriceLiteral.Text = item.PRICE.ToString();
            }
        }

        protected void lvUsers_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                var item = e.Item.DataItem as LeaderboardItem;
                var profileImage = e.Item.FindControl("profileImage") as Image;
                var usernameLiteral = e.Item.FindControl("litUserName") as Literal;
                var pointsLiteral = e.Item.FindControl("litPoints") as Literal;

                profileImage.ImageUrl = $"data:image;base64,{Convert.ToBase64String(item.PROFILE_IMAGE)}";
                usernameLiteral.Text = item.USERNAME;
                pointsLiteral.Text = item.POINTS.ToString();
            }
        }
        protected void lvLeaderboard_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                var item = e.Item.DataItem as LeaderboardItem;
                var profileImage = e.Item.FindControl("profileImage") as Image;
                var usernameLiteral = e.Item.FindControl("usernameLiteral") as Literal;
                var pointsLiteral = e.Item.FindControl("pointsLiteral") as Literal;

                profileImage.ImageUrl = $"data:image;base64,{Convert.ToBase64String(item.PROFILE_IMAGE)}";
                usernameLiteral.Text = item.USERNAME;
                pointsLiteral.Text = item.POINTS.ToString();
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