namespace BeSide.DataAccess.SqlDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class С : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Feedbacks", "Text", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Feedbacks", "Text");
        }
    }
}
