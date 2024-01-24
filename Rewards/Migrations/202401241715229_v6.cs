namespace Rewards.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v6 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Purchases",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DATE = c.DateTime(nullable: false, storeType: "date"),
                        ID_USER = c.Int(nullable: false),
                        ID_REWARD = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Rewards", t => t.ID_REWARD, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.ID_USER, cascadeDelete: true)
                .Index(t => t.ID_USER)
                .Index(t => t.ID_REWARD);
            
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
            DropForeignKey("dbo.Purchases", "ID_USER", "dbo.Users");
            DropForeignKey("dbo.Purchases", "ID_REWARD", "dbo.Rewards");
            DropIndex("dbo.Purchases", new[] { "ID_REWARD" });
            DropIndex("dbo.Purchases", new[] { "ID_USER" });
            DropTable("dbo.Rewards");
            DropTable("dbo.Purchases");
        }
    }
}
