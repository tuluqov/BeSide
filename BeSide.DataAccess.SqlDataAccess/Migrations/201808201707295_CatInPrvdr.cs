namespace BeSide.DataAccess.SqlDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CatInPrvdr : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Categories", "ProviderProfile_Id", "dbo.ProviderProfiles");
            DropIndex("dbo.Categories", new[] { "ProviderProfile_Id" });
            DropColumn("dbo.Categories", "ProviderProfile_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "ProviderProfile_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Categories", "ProviderProfile_Id");
            AddForeignKey("dbo.Categories", "ProviderProfile_Id", "dbo.ProviderProfiles", "Id");
        }
    }
}
