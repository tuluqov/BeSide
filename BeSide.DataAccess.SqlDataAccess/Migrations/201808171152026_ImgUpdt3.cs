namespace BeSide.DataAccess.SqlDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImgUpdt3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "ClientProfileId", c => c.String());
            AddColumn("dbo.Images", "ProviderProfileId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Images", "ProviderProfileId");
            DropColumn("dbo.Images", "ClientProfileId");
        }
    }
}
