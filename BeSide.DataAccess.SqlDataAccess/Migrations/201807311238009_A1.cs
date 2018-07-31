namespace BeSide.DataAccess.SqlDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class A1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BaseProfiles", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.BaseProfiles", new[] { "Id" });
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
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.BaseProfiles", "Id");
            AddForeignKey("dbo.BaseProfiles", "Id", "dbo.AspNetUsers", "Id");
        }
    }
}
