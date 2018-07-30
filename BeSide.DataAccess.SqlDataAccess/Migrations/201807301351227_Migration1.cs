namespace BeSide.DataAccess.SqlDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ProviderProfiles", "ApplicationUserId");
            DropColumn("dbo.ClientProfiles", "ApplicationUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ClientProfiles", "ApplicationUserId", c => c.Int(nullable: false));
            AddColumn("dbo.ProviderProfiles", "ApplicationUserId", c => c.Int(nullable: false));
        }
    }
}
