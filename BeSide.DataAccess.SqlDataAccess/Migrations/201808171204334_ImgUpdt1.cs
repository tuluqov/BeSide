namespace BeSide.DataAccess.SqlDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImgUpdt1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Images", "ClientProfileId");
            DropColumn("dbo.Images", "ProviderProfileId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Images", "ProviderProfileId", c => c.String());
            AddColumn("dbo.Images", "ClientProfileId", c => c.String());
        }
    }
}
