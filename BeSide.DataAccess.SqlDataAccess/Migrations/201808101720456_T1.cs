namespace BeSide.DataAccess.SqlDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class T1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProviderProfiles", "Discription", c => c.String(maxLength: 300));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProviderProfiles", "Discription");
        }
    }
}
