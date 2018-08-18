namespace BeSide.DataAccess.SqlDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImgUpdt4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Images", "ApplicationUser_Id");
            AddForeignKey("dbo.Images", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Images", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Images", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Images", "ApplicationUser_Id");
        }
    }
}
