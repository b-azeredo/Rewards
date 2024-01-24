namespace Rewards.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Purchases", "ID_REWARD", "dbo.Rewards");
            DropForeignKey("dbo.Purchases", "ID_USER", "dbo.Users");
            DropIndex("dbo.Purchases", new[] { "ID_USER" });
            DropIndex("dbo.Purchases", new[] { "ID_REWARD" });
            DropTable("dbo.Purchases");
            DropTable("dbo.Rewards");
        }
        
        public override void Down()
        {
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
            
            CreateTable(
                "dbo.Purchases",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DATE = c.DateTime(nullable: false, storeType: "date"),
                        ID_USER = c.Int(nullable: false),
                        ID_REWARD = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateIndex("dbo.Purchases", "ID_REWARD");
            CreateIndex("dbo.Purchases", "ID_USER");
            AddForeignKey("dbo.Purchases", "ID_USER", "dbo.Users", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Purchases", "ID_REWARD", "dbo.Rewards", "ID", cascadeDelete: true);
        }
    }
}
