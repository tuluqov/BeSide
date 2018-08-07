namespace BeSide.DataAccess.SqlDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class C : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Feedbacks", "Price", c => c.Single(nullable: false));
            AlterColumn("dbo.Orders", "Price", c => c.Single());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "Price", c => c.Int());
            DropColumn("dbo.Feedbacks", "Price");
        }
    }
}
