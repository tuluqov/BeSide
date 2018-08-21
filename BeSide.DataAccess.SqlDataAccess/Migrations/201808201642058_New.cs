namespace BeSide.DataAccess.SqlDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class New : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Images", "Order_Id", "dbo.Orders");
            DropIndex("dbo.Images", new[] { "Order_Id" });
            RenameColumn(table: "dbo.Images", name: "Order_Id", newName: "OrderId");
            AlterColumn("dbo.Images", "OrderId", c => c.Int(nullable: false));
            CreateIndex("dbo.Images", "OrderId");
            AddForeignKey("dbo.Images", "OrderId", "dbo.Orders", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Images", "OrderId", "dbo.Orders");
            DropIndex("dbo.Images", new[] { "OrderId" });
            AlterColumn("dbo.Images", "OrderId", c => c.Int());
            RenameColumn(table: "dbo.Images", name: "OrderId", newName: "Order_Id");
            CreateIndex("dbo.Images", "Order_Id");
            AddForeignKey("dbo.Images", "Order_Id", "dbo.Orders", "Id");
        }
    }
}
