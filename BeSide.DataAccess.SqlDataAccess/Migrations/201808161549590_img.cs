namespace BeSide.DataAccess.SqlDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class img : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImageData = c.Binary(),
                        ImageMimeType = c.String(),
                        Order_Id = c.Int(),
                        ProviderProfile_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.Order_Id)
                .ForeignKey("dbo.ProviderProfiles", t => t.ProviderProfile_Id)
                .Index(t => t.Order_Id)
                .Index(t => t.ProviderProfile_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Images", "ProviderProfile_Id", "dbo.ProviderProfiles");
            DropForeignKey("dbo.Images", "Order_Id", "dbo.Orders");
            DropIndex("dbo.Images", new[] { "ProviderProfile_Id" });
            DropIndex("dbo.Images", new[] { "Order_Id" });
            DropTable("dbo.Images");
        }
    }
}
