namespace BeSide.DataAccess.SqlDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImgUpdt : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Images", "ProviderProfile_Id", "dbo.ProviderProfiles");
            DropIndex("dbo.Images", new[] { "ProviderProfile_Id" });
            AddColumn("dbo.Images", "FileName", c => c.String(maxLength: 255));
            AddColumn("dbo.Images", "ContentType", c => c.String(maxLength: 100));
            AddColumn("dbo.Images", "Content", c => c.Binary());
            AddColumn("dbo.Images", "FileType", c => c.Int(nullable: false));
            AddColumn("dbo.Images", "ProfileId", c => c.String(maxLength: 128));
            DropColumn("dbo.Images", "ImageData");
            DropColumn("dbo.Images", "ImageMimeType");
            DropColumn("dbo.Images", "ProviderProfile_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Images", "ProviderProfile_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Images", "ImageMimeType", c => c.String());
            AddColumn("dbo.Images", "ImageData", c => c.Binary());
            DropColumn("dbo.Images", "ProfileId");
            DropColumn("dbo.Images", "FileType");
            DropColumn("dbo.Images", "Content");
            DropColumn("dbo.Images", "ContentType");
            DropColumn("dbo.Images", "FileName");
            CreateIndex("dbo.Images", "ProviderProfile_Id");
            AddForeignKey("dbo.Images", "ProviderProfile_Id", "dbo.ProviderProfiles", "Id");
        }
    }
}
