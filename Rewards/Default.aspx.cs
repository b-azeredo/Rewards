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
                // Chama o método para obter os itens da leaderboard do banco de dados
                var leaderboardItems = LeaderboardManager.GetLeaderboardItemsFromDatabase();

                // Define a lista de itens como a fonte de dados da ListView
                lvLeaderboard.DataSource = leaderboardItems;

                // Atualiza a ListView
                lvLeaderboard.DataBind();
            }
        }
    }
}
