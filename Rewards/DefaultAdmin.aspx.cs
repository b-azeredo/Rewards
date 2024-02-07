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

            if (UserManager.Get_Role(int.Parse(idUser.Value)) != "EMPLOYEE")
            {
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

            if (string.IsNullOrWhiteSpace(newActivityName.Text) || string.IsNullOrWhiteSpace(newActivityDescription.Text) || string.IsNullOrWhiteSpace(newActivityPoints.Text))
            {
                string script = "messageAlert('Please, fill all the fields');";
                ClientScript.RegisterStartupScript(this.GetType(), "ValidationAlert", script, true);
                return;
            }

            int activityId = int.Parse(hiddenActivityID.Value);

            using (var entities = new Entities2())
            {
                ACTIVITY activity = entities.ACTIVITY.FirstOrDefault(x => x.ID == activityId);

                if (activity.POINTS == int.Parse(newActivityPoints.Text))
                {
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
                else
                {
                    ACTIVITY activityItem = new ACTIVITY()
                    {
                        NAME = newActivityName.Text,
                        DESCRIPTION = newActivityDescription.Text,
                        POINTS = int.Parse(newActivityPoints.Text),
                        ACTIVATED = bool.Parse(dlActivityStatus.SelectedValue)
                    };

                    activity.ACTIVATED = false;
                    entities.ACTIVITY.Add(activityItem);
                    entities.SaveChanges();
                    Reload();
                    string script = "messageAlert('Your changes have been saved!');";
                    ClientScript.RegisterStartupScript(this.GetType(), "ValidationAlert", script, true);
                }
            }
        }

        protected void btnComfirmUserChanges_Click(object sender, EventArgs e)
        {
            int userId = int.Parse(ID.Text);

            if (string.IsNullOrWhiteSpace(newName.Text) || string.IsNullOrWhiteSpace(newEmail.Text))
            {
                string script = "messageAlert('Please fill in all required fields.');";
                ClientScript.RegisterStartupScript(this.GetType(), "ValidationAlert", script, true);
                return;
            }

            if (!IsValidEmail(newEmail.Text))
            {
                string script = "messageAlert('Please enter a valid email address for the user.');";
                ClientScript.RegisterStartupScript(this.GetType(), "ValidationAlert", script, true);
                return;
            }

            if (dlRole.SelectedValue == "EMPLOYEE")
            {
                if (!IsManagerEmailValid(newEmail.Text))
                {
                    string script = "<script>messageAlert('Please enter a manager email that exists.');</script>";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowError", script);
                    return;
                }
            }

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
                        if (string.IsNullOrWhiteSpace(newManagerEmail.Text))
                        {
                            string script2 = "messageAlert('Please, fill the manager email field.');";
                            ClientScript.RegisterStartupScript(this.GetType(), "ValidationAlert", script2, true);
                            return;
                        }
                        user.MANAGER_EMAIL = newManagerEmail.Text;
                    }
                    else
                    {
                        user.MANAGER_EMAIL = null;
                    }
                    string fileExtension = System.IO.Path.GetExtension(userFileUpload.FileName).ToLower();
                    if (userFileUpload.HasFile)
                    {
                        if (fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png" || fileExtension == ".gif")
                        {
                            user.PROFILE_IMAGE = userFileUpload.FileBytes;
                        }
                        else
                        {
                            string script2 = "messageAlert('Image file type invalid!');";
                            ClientScript.RegisterStartupScript(this.GetType(), "ValidationAlert", script2, true);
                            return;
                        }
                    }
                    string script = "messageAlert('Your changes have been saved!');";
                    ClientScript.RegisterStartupScript(this.GetType(), "ValidationAlert", script, true);
                    entities.SaveChanges();
                    Reload();
                }
            }
        }



        protected void btnComfirmRewardChanges_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRewardName.Text) || string.IsNullOrWhiteSpace(txtRewardPrice.Text))
            {
                string script = "messageAlert('Please, fill all fields.');";
                ClientScript.RegisterStartupScript(this.GetType(), "ValidationAlert", script, true);
                return;
            }
            int rewardId = int.Parse(rewardID.Value);
            using (var entities = new Entities2())
            {
                REWARD reward = entities.REWARD.FirstOrDefault(x => x.ID == rewardId);
                if (reward != null)
                {
                    if (rewardFileUpload.HasFile)
                    {
                        string fileExtension = System.IO.Path.GetExtension(rewardFileUpload.FileName).ToLower();
                        if (fileExtension != ".jpg" && fileExtension != ".jpeg" && fileExtension != ".png" && fileExtension != ".gif")
                        {
                            string script2 = "messageAlert('Image file type invalid! Please select a valid image file.');";
                            ClientScript.RegisterStartupScript(this.GetType(), "ValidationAlert", script2, true);
                            return;
                        }
                    }
                    if (txtInsertStock.Text != "")
                    {
                        int stock;
                        if (int.TryParse(txtInsertStock.Text, out stock))
                        {
                            REWARD_STOCK rewardStock = new REWARD_STOCK()
                            {
                                REWARD_ID = rewardId,
                                STOCK = stock,
                                DATE = DateTime.Now,
                            };
                            entities.REWARD_STOCK.Add(rewardStock);
                            entities.SaveChanges();
                        }
                    }

                    if (reward.PRICE != int.Parse(txtRewardPrice.Text))
                    {
                        int previousStock = RewardsManager.GetRewardStock(reward.ID);
                        byte[] image = null;
                        if (rewardFileUpload.HasFile)
                        {
                            int maxSizeInBytes = 1024 * 1024; // 1 MB
                            byte[] imageData = rewardFileUpload.FileBytes;
                            if (imageData.Length <= maxSizeInBytes)
                            {
                                image = imageData;
                            }
                        }
                        var newReward = new REWARD()
                        {
                            NAME = txtRewardName.Text,
                            PRICE = int.Parse(txtRewardPrice.Text),
                            ACTIVATED = bool.Parse(dlRewardStatus.SelectedValue),
                            IMAGE = image ?? reward.IMAGE
                        };
                        entities.REWARD.Add(newReward);
                        entities.SaveChanges();
                        int newRewardId = newReward.ID;
                        RewardsManager.ClearRewardStock(rewardId);
                        RewardsManager.ChangeStatus(rewardId);
                        var rewardStock = new REWARD_STOCK()
                        {
                            REWARD_ID = newRewardId,
                            STOCK = previousStock
                        };
                        entities.REWARD_STOCK.Add(rewardStock);
                        entities.SaveChanges();
                    }
                    else
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
                        }
                        entities.SaveChanges();
                    }
                    Reload();
                    string script = "messageAlert('Your changes have been saved!');";
                    ClientScript.RegisterStartupScript(this.GetType(), "ValidationAlert", script, true);
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
                Reload();
                string script = "messageAlert('Activity added successfully');";
                ClientScript.RegisterStartupScript(this.GetType(), "ValidationAlert", script, true);
            }
        }

        protected void btnComfirmAddUser_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UserName.Text))
            {
                string script = "<script>messageAlert('Please enter a valid name.');</script>";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowError", script);
                return;
            }

            if (string.IsNullOrWhiteSpace(UserEmail.Text) || !IsValidEmail(UserEmail.Text) || UserManager.Email_Exists(UserEmail.Text))
            {
                string script = "<script>messageAlert('Please enter a valid email.');</script>";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowError", script);
                return;
            }

            if (FileUpload1.PostedFile == null || FileUpload1.PostedFile.InputStream == null)
            {
                string script = "<script>messageAlert('Please enter a valid image.');</script>";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowError", script);
                return;
            }

            string fileExtension = Path.GetExtension(FileUpload1.FileName).ToLower();
            if (fileExtension != ".png" && fileExtension != ".jpg" && fileExtension != ".jpeg" && fileExtension != ".gif")
            {
                string script = "<script>messageAlert('Please enter a valid image extension(.png .jpg .jpeg .gif)');</script>";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowError", script);
                return;
            }

            if (dlRoleUser.SelectedValue == "EMPLOYEE" && !IsManagerEmailValid(managerEmailTextBox.Text))
            {
                string script = "<script>messageAlert('Please enter a manager email that exists.');</script>";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowError", script);
                return;
            }

            using (Entities2 entities2 = new Entities2())
            {
                USER newuser = new USER
                {
                    NAME = UserName.Text,
                    EMAIL = UserEmail.Text,
                    ROLE = dlRoleUser.SelectedValue,
                    PROFILE_IMAGE = FileUpload1.FileBytes,
                    MANAGER_EMAIL = dlRoleUser.SelectedValue == "EMPLOYEE" ? managerEmailTextBox.Text : null,
                    ACTIVATED = true
                };
                entities2.USER.Add(newuser);
                entities2.SaveChanges();
                Reload();
                string script = "messageAlert('User added successfully');";
                ClientScript.RegisterStartupScript(this.GetType(), "ValidationAlert", script, true);
            }
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        private bool IsManagerEmailValid(string managerEmail)
        {
            using (Entities2 entities2 = new Entities2())
            {
                return entities2.USER.Any(u => u.EMAIL == managerEmail && u.ROLE == "MANAGER");
            }
        }

        protected void btnComfirmAddReward_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txbRewardName.Text))
            {
                string script = "messageAlert('Please, insert a name in the reward');";
                ClientScript.RegisterStartupScript(this.GetType(), "ValidationAlert", script, true);
                return;
            }

            if (!int.TryParse(txbRewardPrice.Text, out _))
            {
                string script = "messageAlert('Please, use a valid price');";
                ClientScript.RegisterStartupScript(this.GetType(), "ValidationAlert", script, true);
                return;
            }

            if (RewardImage.HasFile)
            {
                string fileExtension = System.IO.Path.GetExtension(RewardImage.FileName).ToLower();
                if (fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png" || fileExtension == ".gif")
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
                        Reload();
                        string script = "messageAlert('Reward added successfully');";
                        ClientScript.RegisterStartupScript(this.GetType(), "ValidationAlert", script, true);
                    };
                }
                else
                {
                    string script = "messageAlert('Please, insert a valid image');";
                    ClientScript.RegisterStartupScript(this.GetType(), "ValidationAlert", script, true);
                    return;
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

                activityIDLiteral.Value = item.ACTIVITY_ID.ToString();
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


        private void Reload()
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
            var userItems = UserManager.GetUsersFromDatabase();
            lvUsers.DataSource = userItems;
            lvUsers.DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "clearModalFields", "clearModalFields();", true);
            if (!IsPostBack)
            {
                Reload();
            }
        }


    }
}