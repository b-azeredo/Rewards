namespace Rewards.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v3 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Purchases", new[] { "REWARD_ID" });
            DropIndex("dbo.Purchases", new[] { "USER_ID" });
            CreateIndex("dbo.Purchases", "Reward_ID");
            CreateIndex("dbo.Purchases", "User_ID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Purchases", new[] { "User_ID" });
            DropIndex("dbo.Purchases", new[] { "Reward_ID" });
            CreateIndex("dbo.Purchases", "USER_ID");
            CreateIndex("dbo.Purchases", "REWARD_ID");
        }
    }
}
