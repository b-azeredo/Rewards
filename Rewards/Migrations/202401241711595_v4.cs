namespace Rewards.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Purchases", "User_ID", "dbo.Users");
            DropForeignKey("dbo.Purchases", "Reward_ID", "dbo.Rewards");
            DropIndex("dbo.Purchases", new[] { "Reward_ID" });
            DropIndex("dbo.Purchases", new[] { "User_ID" });
            RenameColumn(table: "dbo.Purchases", name: "User_ID", newName: "ID_USER");
            RenameColumn(table: "dbo.Purchases", name: "Reward_ID", newName: "ID_REWARD");
            AlterColumn("dbo.Purchases", "ID_REWARD", c => c.Int(nullable: false));
            AlterColumn("dbo.Purchases", "ID_USER", c => c.Int(nullable: false));
            CreateIndex("dbo.Purchases", "ID_USER");
            CreateIndex("dbo.Purchases", "ID_REWARD");
            AddForeignKey("dbo.Purchases", "ID_USER", "dbo.Users", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Purchases", "ID_REWARD", "dbo.Rewards", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Purchases", "ID_REWARD", "dbo.Rewards");
            DropForeignKey("dbo.Purchases", "ID_USER", "dbo.Users");
            DropIndex("dbo.Purchases", new[] { "ID_REWARD" });
            DropIndex("dbo.Purchases", new[] { "ID_USER" });
            AlterColumn("dbo.Purchases", "ID_USER", c => c.Int());
            AlterColumn("dbo.Purchases", "ID_REWARD", c => c.Int());
            RenameColumn(table: "dbo.Purchases", name: "ID_REWARD", newName: "Reward_ID");
            RenameColumn(table: "dbo.Purchases", name: "ID_USER", newName: "User_ID");
            CreateIndex("dbo.Purchases", "User_ID");
            CreateIndex("dbo.Purchases", "Reward_ID");
            AddForeignKey("dbo.Purchases", "Reward_ID", "dbo.Rewards", "ID");
            AddForeignKey("dbo.Purchases", "User_ID", "dbo.Users", "ID");
        }
    }
}
