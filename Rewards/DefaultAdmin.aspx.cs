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
using System.Web.UI.HtmlControls;

namespace Rewards
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        /* Open Modal Click */

        protected void btnEditActivity_Click(object sender, EventArgs e)
        {
            Button btnEditActivity = (Button)sender;
            ListViewItem item = (ListViewItem)btnEditActivity.NamingContainer;

            HiddenField activityID = (HiddenField)item.FindControl("activityIdLiteral");
            Literal activityNameLiteral = (Literal)item.FindControl("activityNameLiteral");

            newActivityName.Text = activityNameLiteral.Text.ToString();
            newActivityDescription.Text = ActivitiesManager.Get_Description(int.Parse(activityID.Value));
            newActivityPoints.Text = ActivitiesManager.Get_Points(int.Parse(activityID.Value)).ToString();
            hiddenActivityID.Value = activityID.Value;

            foreach (ListItem dlItem in dlActivityStatus.Items)
            {
                dlItem.Selected = false;
            }

            bool activated = ActivitiesManager.Get_Activated(int.Parse(activityID.Value));
            dlActivityStatus.Items.FindByValue(activated.ToString()).Selected = true;



            Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "showEditActivityModal()", true);
        }

        protected void btnEditReward_Click(object sender, EventArgs e)
        {
            Button btnEditReward = (Button)sender;
            ListViewItem item = (ListViewItem)btnEditReward.NamingContainer;

            Image rewardImage = (Image)item.FindControl("rewardImage");
            Literal rewardNameLiteral = (Literal)item.FindControl("rewardNameLiteral");
            Literal rewardPriceLiteral = (Literal)item.FindControl("rewardPriceLiteral");
            HiddenField rewardId = (HiddenField)item.FindControl("rewardIDHiddenField");
            HiddenField rewardStatusHiddenField = (HiddenField)item.FindControl("rewardStatusHiddenField");

            rewardID.Value = rewardId.Value.ToString();
            RewardImagePlaceholder.Src = rewardImage.ImageUrl;
            txtRewardName.Text = rewardNameLiteral.Text;
            txtRewardPrice.Text = rewardPriceLiteral.Text;
            labelCurrentStock.Text = RewardsManager.GetRewardStock(int.Parse(rewardId.Value)).ToString();
            foreach (ListItem dlItem in dlRewardStatus.Items)
            {
                dlItem.Selected = false;
            }
            dlRewardStatus.Items.FindByValue(rewardStatusHiddenField.Value).Selected = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "showEditRewardModal()", true);
        }

        protected void btnEditUser_Click(object sender, EventArgs e)
        {
            Button btnEditUser = (Button)sender;
            ListViewItem item = (ListViewItem)btnEditUser.NamingContainer;

            Image profileImage = (Image)item.FindControl("profileImage");
            Literal litUserName = (Literal)item.FindControl("litUserName");
            HiddenField idUser = (HiddenField)item.FindControl("userId");

            ID.Text = idUser.Value;
            newName.Text = litUserName.Text;
            UserImagePlaceholder.Src = profileImage.ImageUrl;
            newEmail.Text = UserManager.Get_Email(int.Parse(idUser.Value));
            newManagerEmail.Text = UserManager.Get_Manager_Email(int.Parse(idUser.Value));

            foreach (ListItem dlItem in dlRole.Items)
            {
                dlItem.Selected = false;
            }

            foreach (ListItem dlItem in dlUserActivated.Items)
            {
                dlItem.Selected = false;
            }

            dlRole.Items.FindByValue(UserManager.Get_Role(int.Parse(idUser.Value))).Selected = true;
            dlUserActivated.Items.FindByValue(UserManager.Get_Activated(int.Parse(idUser.Value)).ToString()).Selected = true;

            if (UserManager.Get_Role(int.Parse(idUser.Value)) != "EMPLOYEE"){
                newManagerEmail.Style["display"] = "none";
                managerEmailLabel.Style["display"] = "none";
            }
            else
            {
                newManagerEmail.Style["display"] = "block";
                managerEmailLabel.Style["display"] = "block";
            }

            Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "showEditUserModal()", true);
        }

        /* COMFIRM CHANGES */

        protected void btnComfirmActivityChanges_Click(object sender, EventArgs e)
        {
            int activityId = int.Parse(hiddenActivityID.Value);

            using (var entities = new Entities2())
            {
                ACTIVITY activity = entities.ACTIVITY.FirstOrDefault(x => x.ID == activityId);

                if (activity != null)
                {
                    activity.NAME = newActivityName.Text;
                    activity.DESCRIPTION = newActivityDescription.Text;
                    activity.POINTS = int.Parse(newActivityPoints.Text);
                    activity.ACTIVATED = bool.Parse(dlActivityStatus.SelectedValue);

                    entities.SaveChanges();
                    Response.Redirect(Request.RawUrl);
                }
            }
        }

        protected void btnComfirmUserChanges_Click(object sender, EventArgs e)
        {
            int userId = int.Parse(ID.Text);

            using (var entities = new Entities2())
            {
                USER user = entities.USER.FirstOrDefault(x => x.ID == userId);

                if (user != null)
                {
                    user.NAME = newName.Text;
                    user.EMAIL = newEmail.Text;
                    user.ROLE = dlRole.SelectedValue;
                    user.ACTIVATED = bool.Parse(dlUserActivated.SelectedValue);


                    if (user.ROLE == "EMPLOYEE")
                    {
                        user.MANAGER_EMAIL = newManagerEmail.Text;
                    }
                    else
                    {
                        user.MANAGER_EMAIL = null;
                    }

                    if (userFileUpload.HasFile)
                    {
                        byte[] imageData = userFileUpload.FileBytes;
                        user.PROFILE_IMAGE = imageData;
                    }

                    entities.SaveChanges();
                    Response.Redirect(Request.RawUrl);
                }
            }
        }


        protected void btnComfirmRewardChanges_Click(object sender, EventArgs e)
        {
            int rewardId = int.Parse(rewardID.Value);

            using (var entities = new Entities2())
            {
                REWARD reward = entities.REWARD.FirstOrDefault(x => x.ID == rewardId);

                if (txtInsertStock.Text != "")
                {
                    REWARD_STOCK rewardStock = new REWARD_STOCK()
                    {
                        REWARD_ID = rewardId,
                        STOCK = int.Parse(txtInsertStock.Text),
                    };
                    entities.REWARD_STOCK.Add(rewardStock);
                }

                if (reward != null)
                {
                    reward.NAME = txtRewardName.Text;
                    reward.PRICE = int.Parse(txtRewardPrice.Text);
                    reward.ACTIVATED = bool.Parse(dlRewardStatus.SelectedValue);
                    if (rewardFileUpload.HasFile)
                    {
                        int maxSizeInBytes = 1024 * 1024; // 1 MB
                        byte[] imageData = rewardFileUpload.FileBytes;
                        if (imageData.Length <= maxSizeInBytes)
                        {
                            reward.IMAGE = imageData;
                        }
                        else
                        {
                            // Lógica para lidar com o tamanho da imagem excedido
                        }
                    }

                    entities.SaveChanges();
                    Response.Redirect(Request.RawUrl);
                }
            }
        }


        /* COMFIRM ADD */

        protected void btnComfirmAddActivity_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ActivityName.Text) || string.IsNullOrWhiteSpace(ActivityDescription.Text) || string.IsNullOrWhiteSpace(ActivityPoints.Text))
            {
                string script = "<script>messageAlert('Please fill in all required fields.');</script>";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowError", script);
                return;
            }
            if (!int.TryParse(ActivityPoints.Text, out int points))
            {
                string script = "<script>messageAlert('Please enter a valid integer value for points.');</script>";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowError", script);
                return;
            }

            using (Entities2 entities2 = new Entities2())
            {
                ACTIVITY newActivity = new ACTIVITY()
                {
                    NAME = ActivityName.Text,
                    DESCRIPTION = ActivityDescription.Text,
                    ACTIVATED = true,
                    POINTS = int.Parse(ActivityPoints.Text),
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
                var activityIDLiteral = e.Item.FindControl("activityIdLiteral") as HiddenField;

                activityIDLiteral.Value  = item.ACTIVITY_ID.ToString();
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
                var rewardIdHiddenField = e.Item.FindControl("rewardIDHiddenField") as HiddenField;
                var rewardStatusHiddenField = e.Item.FindControl("rewardStatusHiddenField") as HiddenField;


                rewardIdHiddenField.Value = item.ID.ToString();
                rewardStatusHiddenField.Value = item.ACTIVATED.ToString();
                rewardImage.ImageUrl = $"data:image;base64,{Convert.ToBase64String(item.IMAGE)}";
                rewardNameLiteral.Text = item.NAME;
                rewardPriceLiteral.Text = item.PRICE.ToString();
            }
        }

        protected void lvUsers_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                var userData = e.Item.DataItem as LeaderboardItem;
                var profileImage = e.Item.FindControl("profileImage") as Image;
                var litUserName = e.Item.FindControl("litUserName") as Literal;
                var litPoints = e.Item.FindControl("litPoints") as Literal;
                var userid = e.Item.FindControl("userId") as HiddenField;

                userid.Value = userData.ID.ToString();
                profileImage.ImageUrl = $"data:image;base64,{Convert.ToBase64String(userData.PROFILE_IMAGE)}";
                litUserName.Text = userData.USERNAME;
                litPoints.Text = userData.POINTS.ToString();
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
                ScriptManager.RegisterStartupScript(this, GetType(), "clearModalFields", "clearModalFields();", true);
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
                var userItems = UserManager.GetUsersFromDatabase();
                lvUsers.DataSource = userItems;
                lvUsers.DataBind();

            }
        }


    }
}