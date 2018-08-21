namespace BeSide.DataAccess.SqlDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class New2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Images", "OrderId", "dbo.Orders");
            DropIndex("dbo.Images", new[] { "OrderId" });
            AlterColumn("dbo.Images", "OrderId", c => c.Int());
            CreateIndex("dbo.Images", "OrderId");
            AddForeignKey("dbo.Images", "OrderId", "dbo.Orders", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Images", "OrderId", "dbo.Orders");
            DropIndex("dbo.Images", new[] { "OrderId" });
            AlterColumn("dbo.Images", "OrderId", c => c.Int(nullable: false));
            CreateIndex("dbo.Images", "OrderId");
            AddForeignKey("dbo.Images", "OrderId", "dbo.Orders", "Id", cascadeDelete: true);
        }
    }
}
