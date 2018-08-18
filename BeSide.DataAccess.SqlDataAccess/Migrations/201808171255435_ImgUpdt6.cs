namespace BeSide.DataAccess.SqlDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImgUpdt6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProviderProfiles", "FileName", c => c.String(maxLength: 255));
            AddColumn("dbo.ProviderProfiles", "ContentType", c => c.String(maxLength: 100));
            AddColumn("dbo.ProviderProfiles", "Content", c => c.Binary());
            AddColumn("dbo.ProviderProfiles", "FileType", c => c.Int(nullable: false));
            AddColumn("dbo.ClientProfiles", "FileName", c => c.String(maxLength: 255));
            AddColumn("dbo.ClientProfiles", "ContentType", c => c.String(maxLength: 100));
            AddColumn("dbo.ClientProfiles", "Content", c => c.Binary());
            AddColumn("dbo.ClientProfiles", "FileType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ClientProfiles", "FileType");
            DropColumn("dbo.ClientProfiles", "Content");
            DropColumn("dbo.ClientProfiles", "ContentType");
            DropColumn("dbo.ClientProfiles", "FileName");
            DropColumn("dbo.ProviderProfiles", "FileType");
            DropColumn("dbo.ProviderProfiles", "Content");
            DropColumn("dbo.ProviderProfiles", "ContentType");
            DropColumn("dbo.ProviderProfiles", "FileName");
        }
    }
}
