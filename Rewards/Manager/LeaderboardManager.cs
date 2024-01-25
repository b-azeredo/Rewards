using Rewards.DB_Models;
using Rewards.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rewards.Manager
{
    public class LeaderboardManager
    {
        public static List<LeaderboardItem> GetLeaderboardItemsFromDatabase()
        {
            using (var context = new Context())
            {
                // Recupera os usuários ordenados por pontos em ordem decrescente
                List<User> users = context.Users.OrderByDescending(u => u.POINTS).ToList();

                // Converte os usuários em objetos LeaderboardItem
                List<LeaderboardItem> leaderboardItems = users.Select(u => new LeaderboardItem
                {
                    USERNAME = u.NAME,
                    POINTS = u.POINTS
                }).ToList();

                return leaderboardItems;
            }
        }
    }
}