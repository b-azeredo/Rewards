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
        protected void btnHide_Command(object sender, EventArgs e)
        {

        }

        protected void btnComfirmAddReward_Click(object sender, EventArgs e)
        {
            if (RewardImage.HasFile)
            {
                string fileExtension = System.IO.Path.GetExtension(RewardImage.FileName).ToLower();
                if (fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png")
                {
                    // Faça o processamento do arquivo aqui
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                using (Entities2 entities2 = new Entities2())
                {
                    var REWARD = new REWARD_STOCK()
                    {
                        REWARD_ID = 5,
                        STOCK = 20
                    };
                    entities2.REWARD_STOCK.Add(REWARD);
                    entities2.SaveChanges();
                }


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

            }
        }

    }
}