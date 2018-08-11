namespace BeSide.DataAccess.SqlDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class T : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "Name", c => c.String(maxLength: 100));
            AlterColumn("dbo.Services", "Name", c => c.String(maxLength: 100));
            AlterColumn("dbo.ProviderProfiles", "CompanyName", c => c.String(maxLength: 100));
            AlterColumn("dbo.Feedbacks", "Text", c => c.String(nullable: false, maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Feedbacks", "Text", c => c.String(nullable: false));
            AlterColumn("dbo.ProviderProfiles", "CompanyName", c => c.String());
            AlterColumn("dbo.Services", "Name", c => c.String());
            AlterColumn("dbo.Categories", "Name", c => c.String());
        }
    }
}
