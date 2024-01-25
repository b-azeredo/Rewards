namespace Rewards.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v10 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Forms", name: "ID_ACTIVITY", newName: "ACTIVITY_ID");
            RenameColumn(table: "dbo.Forms", name: "ID_USER", newName: "USER_ID");
            RenameColumn(table: "dbo.Purchases", name: "ID_REWARD", newName: "REWARD_ID");
            RenameColumn(table: "dbo.Purchases", name: "ID_USER", newName: "USER_ID");
            RenameIndex(table: "dbo.Forms", name: "IX_ID_USER", newName: "IX_USER_ID");
            RenameIndex(table: "dbo.Forms", name: "IX_ID_ACTIVITY", newName: "IX_ACTIVITY_ID");
            RenameIndex(table: "dbo.Purchases", name: "IX_ID_USER", newName: "IX_USER_ID");
            RenameIndex(table: "dbo.Purchases", name: "IX_ID_REWARD", newName: "IX_REWARD_ID");
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FORM_ID = c.Int(nullable: false),
                        CONTENT = c.Binary(),
                        NAME = c.String(maxLength: 255),
                        EXTENSION = c.String(maxLength: 5),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Forms", t => t.FORM_ID, cascadeDelete: true)
                .Index(t => t.FORM_ID);
            
            CreateTable(
                "dbo.Reward_Stock",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        REWARD_ID = c.Int(nullable: false),
                        STOCK = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Rewards", t => t.REWARD_ID, cascadeDelete: true)
                .Index(t => t.REWARD_ID);
            
            AddColumn("dbo.Forms", "CREATE_DATE", c => c.DateTime(nullable: false, storeType: "date"));
            AddColumn("dbo.Forms", "MANAGER_DATA_APROVED", c => c.DateTime(nullable: false, storeType: "date"));
            AddColumn("dbo.Users", "MANAGER_EMAIL", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Users", "IMAGE_NAME", c => c.String(maxLength: 255));
            AddColumn("dbo.Users", "IMAGE_EXTENSION", c => c.String(maxLength: 5));
            AddColumn("dbo.Purchases", "PURCHASE_DATE", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.Activities", "NAME", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.Forms", "STATUS", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Users", "NAME", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Users", "EMAIL", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.Forms", "FILES");
            DropColumn("dbo.Forms", "DATE");
            DropColumn("dbo.Users", "PASSWORD");
            DropColumn("dbo.Users", "POINTS");
            DropColumn("dbo.Users", "EMAIL_MANAGER");
            DropColumn("dbo.Purchases", "DATE");
            DropColumn("dbo.Rewards", "STOCK");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rewards", "STOCK", c => c.Int(nullable: false));
            AddColumn("dbo.Purchases", "DATE", c => c.DateTime(nullable: false, storeType: "date"));
            AddColumn("dbo.Users", "EMAIL_MANAGER", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Users", "POINTS", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "PASSWORD", c => c.String(nullable: false, maxLength: 200));
            AddColumn("dbo.Forms", "DATE", c => c.DateTime(nullable: false, storeType: "date"));
            AddColumn("dbo.Forms", "FILES", c => c.Binary());
            DropForeignKey("dbo.Reward_Stock", "REWARD_ID", "dbo.Rewards");
            DropForeignKey("dbo.Files", "FORM_ID", "dbo.Forms");
            DropIndex("dbo.Reward_Stock", new[] { "REWARD_ID" });
            DropIndex("dbo.Files", new[] { "FORM_ID" });
            AlterColumn("dbo.Users", "EMAIL", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Users", "NAME", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Forms", "STATUS", c => c.String());
            AlterColumn("dbo.Activities", "NAME", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Purchases", "PURCHASE_DATE");
            DropColumn("dbo.Users", "IMAGE_EXTENSION");
            DropColumn("dbo.Users", "IMAGE_NAME");
            DropColumn("dbo.Users", "MANAGER_EMAIL");
            DropColumn("dbo.Forms", "MANAGER_DATA_APROVED");
            DropColumn("dbo.Forms", "CREATE_DATE");
            DropTable("dbo.Reward_Stock");
            DropTable("dbo.Files");
            RenameIndex(table: "dbo.Purchases", name: "IX_REWARD_ID", newName: "IX_ID_REWARD");
            RenameIndex(table: "dbo.Purchases", name: "IX_USER_ID", newName: "IX_ID_USER");
            RenameIndex(table: "dbo.Forms", name: "IX_ACTIVITY_ID", newName: "IX_ID_ACTIVITY");
            RenameIndex(table: "dbo.Forms", name: "IX_USER_ID", newName: "IX_ID_USER");
            RenameColumn(table: "dbo.Purchases", name: "USER_ID", newName: "ID_USER");
            RenameColumn(table: "dbo.Purchases", name: "REWARD_ID", newName: "ID_REWARD");
            RenameColumn(table: "dbo.Forms", name: "USER_ID", newName: "ID_USER");
            RenameColumn(table: "dbo.Forms", name: "ACTIVITY_ID", newName: "ID_ACTIVITY");
        }
    }
}
