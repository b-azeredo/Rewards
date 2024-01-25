using Rewards.Manager;
using System;

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

                var RewardsItems = AwardsManager.GetAwardItemFromDatabase();
                lvRewards.DataSource = RewardsItems;
                lvRewards.DataBind();

                var activityItems = ActivitiesManager.GetActivityItemFromDatabase();
                lvActivity.DataSource = activityItems;
                lvActivity.DataBind();
            }
        }
    }
}
