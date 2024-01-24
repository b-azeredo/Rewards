namespace Rewards.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NAME = c.String(nullable: false, maxLength: 50),
                        EMAIL = c.String(nullable: false, maxLength: 50),
                        PASSWORD = c.String(nullable: false, maxLength: 200),
                        POINTS = c.Int(nullable: false),
                        ROLE = c.String(nullable: false, maxLength: 50),
                        EMAIL_MANAGER = c.String(nullable: false, maxLength: 50),
                        PROFILE_IMAGE = c.Binary(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
        }
    }
}
