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
    public partial class _Default : System.Web.UI.Page
    {
        private int USER_ID = 1;


        protected void btnClaim_Click(object sender, EventArgs e)
        {
            Button btnSubmit = (Button)sender;
            ListViewItem item = (ListViewItem)btnSubmit.NamingContainer;
            HiddenField rewardIdLiteral = item.FindControl("rewardIdLiteral") as HiddenField;

            int rewardId = Convert.ToInt32(rewardIdLiteral.Value);

            if (UserManager.AddRewardToUser(rewardId, USER_ID) && UserManager.RemoveOneFromStock(rewardId))
            {

            }
            Response.Redirect(Request.RawUrl);

        }

        protected void btnSubmitActivity_Click(object sender, EventArgs e)
        {
            Button btnSubmit = (Button)sender;
            ListViewItem item = (ListViewItem)btnSubmit.NamingContainer;
            Literal activityNameLiteral = (Literal)item.FindControl("activityNameLiteral");
            HiddenField activityIdLiteral = item.FindControl("activityIdLiteral") as HiddenField;
            HiddenField activityDescriptionLiteral = item.FindControl("activityDescriptionLiteral") as HiddenField;

            activityNAME.InnerText = activityNameLiteral.Text;
            activityID.Value = activityIdLiteral.Value;
            activityDESCRIPTION.InnerText = activityDescriptionLiteral.Value;

            Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "showModal()", true);
        }


        protected void btnSubmitForm_Click(object sender, EventArgs e)
        {
            int activityId = Convert.ToInt32(activityID.Value);

            if (string.IsNullOrWhiteSpace(txtActivityDESCRIPTION.Text))
            {
                string script = "<script>messageAlert('Please give a description to the activity.');</script>";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowError", script);
            }
            else if (fileUpload1.HasFiles)
            {
                using (Entities2 entities = new Entities2())
                {
                    var form = new FORM()
                    {
                        USER_ID = this.USER_ID,
                        ACTIVITY_ID = activityId,
                        DESCRIPTION = txtActivityDESCRIPTION.Text,
                        STATUS = null,
                        CREATE_DATE = DateTime.Now,
                    };
                    entities.FORM.Add(form);
                    entities.SaveChanges();

                    foreach (HttpPostedFile uploadedFile in fileUpload1.PostedFiles)
                    {
                        byte[] fileBytes = new byte[uploadedFile.ContentLength];
                        uploadedFile.InputStream.Read(fileBytes, 0, uploadedFile.ContentLength);

                        string fileName = Path.GetFileNameWithoutExtension(uploadedFile.FileName);
                        string fileExtension = Path.GetExtension(uploadedFile.FileName);

                        var file = new FILE()
                        {
                            FORM_ID = form.ID,
                            CONTENT = fileBytes,
                            FILE_NAME = fileName,
                            FILE_EXTENSION = fileExtension
                        };
                        entities.FILE.Add(file);
                    }
                    entities.SaveChanges();

                    string successScript = "<script>messageAlert('The form was sent successfully. When your manager approves, you will receive your points.');</script>";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowSuccess", successScript);
                }
            }
            else
            {
                string script = "<script>messageAlert('Please upload at least one file.');</script>";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowError", script);
            }
        }


        protected void btnSaveProfileChanges_Click(object sender, EventArgs e)
        {
            if (fileUpload.HasFile)
            {
                int maxSizeInBytes = 1024 * 1024; // 1 MB
                byte[] imageData = fileUpload.FileBytes;
                if (imageData.Length <= maxSizeInBytes)
                {
                    using (var entities = new Entities2())
                    {
                        USER user = entities.USER.FirstOrDefault(x => x.ID == USER_ID);

                        if (user != null)
                        {
                            user.PROFILE_IMAGE = imageData;
                            entities.SaveChanges();
                            Response.Redirect(Request.RawUrl);
                        }
                    }
                }
                else
                {
                    string message = "<script>messageAlert('Upload another image.');</script>";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowMessage", message);
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
                var activityId = e.Item.FindControl("activityIdLiteral") as HiddenField;
                var activityDescription = e.Item.FindControl("activityDescriptionLiteral") as HiddenField;

                activityDescription.Value = item.DESCRIPTION;
                activityId.Value = item.ACTIVITY_ID.ToString();
                activityNameLiteral.Text = item.NAME;
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

        protected void lvRewards_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                var item = e.Item.DataItem as RewardsItem;
                var rewardImage = e.Item.FindControl("rewardImage") as Image;
                var rewardNameLiteral = e.Item.FindControl("rewardNameLiteral") as Literal;
                var rewardPriceLiteral = e.Item.FindControl("rewardPriceLiteral") as Literal;
                var rewardIdLiteral = e.Item.FindControl("rewardIdLiteral") as HiddenField;


                rewardImage.ImageUrl = $"data:image;base64,{Convert.ToBase64String(item.IMAGE)}";
                rewardNameLiteral.Text = item.NAME;
                rewardPriceLiteral.Text = item.PRICE.ToString();
                rewardIdLiteral.Value = item.ID.ToString();
            }
        }

        protected void lvTransactions_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                var item = e.Item.DataItem as TransactionItem;
                var transactionNameLiteral = e.Item.FindControl("transactionNameLiteral") as Literal;
                var itemClassLiteral = e.Item.FindControl("itemClassLiteral") as Literal;
                var pointsLiteral = e.Item.FindControl("pointsLiteral") as Literal;
                var transactionDateLiteral = e.Item.FindControl("transactionDateLiteral") as Literal;

                transactionNameLiteral.Text = item.NAME;
                itemClassLiteral.Text = item.ItemClass;
                pointsLiteral.Text = item.POINTS.ToString();
                transactionDateLiteral.Text = item.DATE.ToString();
            }
        }

        protected void lvProfileTransactions_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                var item = e.Item.DataItem as TransactionItem;
                var transactionNameLiteral = e.Item.FindControl("transactionNameLiteral") as Literal;
                var itemClassLiteral = e.Item.FindControl("itemClassLiteral") as Literal;
                var pointsLiteral = e.Item.FindControl("pointsLiteral") as Literal;
                var transactionDateLiteral = e.Item.FindControl("transactionDateLiteral") as Literal;

                transactionNameLiteral.Text = item.NAME;
                itemClassLiteral.Text = item.ItemClass;
                pointsLiteral.Text = item.POINTS.ToString();
                transactionDateLiteral.Text = item.DATE.ToString();
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
                var RewardsItems = RewardsManager.GetRewardItemsFromDatabase();
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
