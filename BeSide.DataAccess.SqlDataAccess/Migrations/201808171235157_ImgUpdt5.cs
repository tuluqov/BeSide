namespace BeSide.DataAccess.SqlDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImgUpdt5 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Images", "BaseProfile_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Images", "BaseProfile_Id", c => c.String(maxLength: 128));
        }
    }
}
