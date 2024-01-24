namespace Rewards.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Purchases",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DATE = c.DateTime(nullable: false, storeType: "date"),
                        Reward_ID = c.Int(),
                        User_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Rewards", t => t.Reward_ID)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .Index(t => t.Reward_ID)
                .Index(t => t.User_ID);
            
            CreateTable(
                "dbo.Rewards",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NAME = c.String(nullable: false, maxLength: 50),
                        PRICE = c.Int(nullable: false),
                        STOCK = c.Int(nullable: false),
                        IMAGE = c.Binary(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Purchases", "User_ID", "dbo.Users");
            DropForeignKey("dbo.Purchases", "Reward_ID", "dbo.Rewards");
            DropIndex("dbo.Purchases", new[] { "User_ID" });
            DropIndex("dbo.Purchases", new[] { "Reward_ID" });
            DropTable("dbo.Rewards");
            DropTable("dbo.Purchases");
        }
    }
}
