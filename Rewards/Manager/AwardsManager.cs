using Rewards.DB_Models;
using Rewards.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rewards.Manager
{
    public class AwardsManager
    {
        public static List<AwardsItem> GetAwardItemFromDatabase()
        {
            using (Context context = new Context())
            {
                // Recupera as Rewards por PREÇO em ordem decrescente
                List<Reward> rewards = context.Rewards.OrderByDescending(r => r.PRICE).ToList();

                // Converte as Rewards em objetos AwardItem
                List<AwardsItem> awardsItems = rewards.Select(r => new AwardsItem
                {
                    NAME = r.NAME,
                    PRICE = r.PRICE,
                    IMAGE_URL = r.IMAGE_URL
                }).ToList();

                return awardsItems;
            }
        }
    }
}