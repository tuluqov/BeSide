namespace BeSide.DataAccess.SqlDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class A2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BaseProfiles", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Feedbacks", "ProviderProfile_Id", "dbo.BaseProfiles");
            DropForeignKey("dbo.Services", "ProviderProfile_Id", "dbo.BaseProfiles");
            DropIndex("dbo.BaseProfiles", new[] { "Id" });
            DropIndex("dbo.Feedbacks", new[] { "ProviderProfile_Id" });
            DropIndex("dbo.Services", new[] { "ProviderProfile_Id" });
            DropColumn("dbo.Feedbacks", "ProviderProfile_Id");
            DropColumn("dbo.Services", "ProviderProfile_Id");
            DropTable("dbo.BaseProfiles");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.BaseProfiles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Patronymic = c.String(nullable: false, maxLength: 50),
                        CompanyName = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Services", "ProviderProfile_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Feedbacks", "ProviderProfile_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Services", "ProviderProfile_Id");
            CreateIndex("dbo.Feedbacks", "ProviderProfile_Id");
            CreateIndex("dbo.BaseProfiles", "Id");
            AddForeignKey("dbo.Services", "ProviderProfile_Id", "dbo.BaseProfiles", "Id");
            AddForeignKey("dbo.Feedbacks", "ProviderProfile_Id", "dbo.BaseProfiles", "Id");
            AddForeignKey("dbo.BaseProfiles", "Id", "dbo.AspNetUsers", "Id");
        }
    }
}
