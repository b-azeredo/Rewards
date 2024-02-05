using Rewards.Item;
using Rewards.Items;
using Rewards.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Util;

namespace Rewards
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        private int USER_ID = 5;

        protected void btnActivityRequest_Click(object sender, EventArgs e)
        {
            Button btnEditActivity = (Button)sender;
            ListViewItem item = (ListViewItem)btnEditActivity.NamingContainer;

            HiddenField formID = (HiddenField)item.FindControl("FormID");

            requestedActivityName.InnerText = ActivitiesManager.Get_Name(FormManager.Get_Activity_Id(int.Parse(formID.Value)));
            requetedActivityDescription.InnerText = ActivitiesManager.Get_Description(FormManager.Get_Activity_Id(int.Parse(formID.Value)));
            requestorName.Text = UserManager.Get_Username(FormManager.Get_User_Id(int.Parse(formID.Value)));
            requestorDescription.Text = FormManager.Get_Description(int.Parse(formID.Value));

            Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "showModal()", true);
        }

        protected void btnSaveProfileChanges_Click(object sender, EventArgs e)
        {

        }


        protected void btnDenyForm_Click(object sender, EventArgs e)
        {

        }

        protected void btnAcceptForm_Click(object sender, EventArgs e)
        {

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

                rewardImage.ImageUrl = $"data:image;base64,{Convert.ToBase64String(item.IMAGE)}";
                rewardNameLiteral.Text = item.NAME;
                rewardPriceLiteral.Text = item.PRICE.ToString();
            }
        }

        protected void lvActivityRequest_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                var item = e.Item.DataItem as FormItem;
                var userRequestNameLiteral = e.Item.FindControl("userRequestNameLiteral") as Literal;
                var activityRequestNameLiteral = e.Item.FindControl("activityRequestNameLiteral") as Literal;
                var FormID = e.Item.FindControl("FormID") as HiddenField;

                userRequestNameLiteral.Text = UserManager.Get_Username(item.USER_ID);
                activityRequestNameLiteral.Text = ActivitiesManager.Get_Name(item.ACTIVITY_ID);
                FormID.Value = item.ID.ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                /* TEAM LEADERBOARD */
                var leaderboardItems = ManagerManager.GetTeamLeaderboardItemsFromDatabase();
                lvLeaderboard.DataSource = leaderboardItems;
                lvLeaderboard.DataBind();

                /* REWARDS */
                var RewardsItems = RewardsManager.GetRewardItemsFromDatabase();
                lvRewards.DataSource = RewardsItems;
                lvRewards.DataBind();

                /* ACTIVITIES REQUESTED */
                var RequestedActivities = ManagerManager.GetRequestedActivitiesItemsFromDatabase();
                lvActivityRequest.DataSource = RequestedActivities;
                lvActivityRequest.DataBind();

                /* ACTIVITIES */
                var activityItems = ActivitiesManager.GetActivityItemsFromDatabase();
                lvActivity.DataSource = activityItems;
                lvActivity.DataBind();

                /* YOUR TEAM PROGRESS INFO */
                lifetimePoints.InnerText = $"{ManagerManager.Get_Team_Lifetime_Points(USER_ID)}";
                activitiesDone.InnerText = $"{ManagerManager.Get_Team_Number_Activities_Done(USER_ID)}";
                redeemedRewards.InnerText = $"{ManagerManager.Get_Team_Number_Redeemed_Rewards(USER_ID)}";

                /* PROFILE INFO */
                profileImage.Src = "data:image;base64," + Convert.ToBase64String(UserManager.Get_Profile_Image(USER_ID));
                profileUsername.InnerHtml = $"{UserManager.Get_Username(USER_ID)}";
                profilePoints.InnerText = $"{UserManager.Get_Current_Points(USER_ID)}";


                /* PROFILE INFO (Modal Info) */
                profileImage2.Src = "data:image;base64," + Convert.ToBase64String(UserManager.Get_Profile_Image(USER_ID));
                profileUsername2.InnerHtml = $"{UserManager.Get_Username(USER_ID)}";
            }
        }

    }
}