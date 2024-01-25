namespace Rewards.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v71 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rewards", "IMAGE", c => c.Binary());
            DropColumn("dbo.Rewards", "IMAGE_URL");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rewards", "IMAGE_URL", c => c.String());
            DropColumn("dbo.Rewards", "IMAGE");
        }
    }
}
