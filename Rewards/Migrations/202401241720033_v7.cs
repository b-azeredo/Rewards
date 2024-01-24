namespace Rewards.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v7 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NAME = c.String(nullable: false, maxLength: 50),
                        POINTS = c.Int(nullable: false),
                        LIMIT_PER_WEEK = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Forms",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ID_USER = c.Int(nullable: false),
                        ID_ACTIVITY = c.Int(nullable: false),
                        DESCRIPTION = c.String(),
                        FILES = c.Binary(),
                        STATUS = c.String(),
                        DATE = c.DateTime(nullable: false, storeType: "date"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Activities", t => t.ID_ACTIVITY, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.ID_USER, cascadeDelete: true)
                .Index(t => t.ID_USER)
                .Index(t => t.ID_ACTIVITY);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Forms", "ID_USER", "dbo.Users");
            DropForeignKey("dbo.Forms", "ID_ACTIVITY", "dbo.Activities");
            DropIndex("dbo.Forms", new[] { "ID_ACTIVITY" });
            DropIndex("dbo.Forms", new[] { "ID_USER" });
            DropTable("dbo.Forms");
            DropTable("dbo.Activities");
        }
    }
}
