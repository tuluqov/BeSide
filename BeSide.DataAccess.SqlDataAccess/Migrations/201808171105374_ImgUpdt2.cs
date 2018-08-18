namespace BeSide.DataAccess.SqlDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImgUpdt2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "BaseProfile_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.Images", "ProfileId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Images", "ProfileId", c => c.String(maxLength: 128));
            DropColumn("dbo.Images", "BaseProfile_Id");
        }
    }
}
