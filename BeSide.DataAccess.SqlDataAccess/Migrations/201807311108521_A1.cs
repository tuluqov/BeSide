namespace BeSide.DataAccess.SqlDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class A1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProviderServiceses", "ProviderProfileId", "dbo.BaseProfiles");
            DropForeignKey("dbo.ProviderServiceses", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.Orders", "ProviderProfileId", "dbo.BaseProfiles");
            DropForeignKey("dbo.Orders", "ClientProfileId", "dbo.BaseProfiles");
            DropForeignKey("dbo.Feedbacks", "ProviderProfileId", "dbo.BaseProfiles");
            DropIndex("dbo.Orders", new[] { "ProviderProfileId" });
            DropIndex("dbo.Feedbacks", new[] { "ProviderProfileId" });
            DropIndex("dbo.ProviderServiceses", new[] { "ProviderProfileId" });
            DropIndex("dbo.ProviderServiceses", new[] { "ServiceId" });
            RenameColumn(table: "dbo.Feedbacks", name: "ProviderProfileId", newName: "ProviderProfile_Id");
            CreateTable(
                "dbo.ClientProfiles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Patronymic = c.String(nullable: false, maxLength: 50),
                        CompanyName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.ProviderServices",
                c => new
                    {
                        ClientProfileId = c.String(nullable: false, maxLength: 128),
                        ServiceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ClientProfileId, t.ServiceId })
                .ForeignKey("dbo.ClientProfiles", t => t.ClientProfileId, cascadeDelete: true)
                .ForeignKey("dbo.Services", t => t.ServiceId, cascadeDelete: true)
                .Index(t => t.ClientProfileId)
                .Index(t => t.ServiceId);
            
            AddColumn("dbo.Orders", "ExecutorProfileId", c => c.String(maxLength: 128));
            AddColumn("dbo.Orders", "ClientProfile_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Feedbacks", "ClientProfileId", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Services", "ProviderProfile_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Feedbacks", "ProviderProfile_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Feedbacks", "ClientProfileId");
            CreateIndex("dbo.Feedbacks", "ProviderProfile_Id");
            CreateIndex("dbo.Orders", "ExecutorProfileId");
            CreateIndex("dbo.Orders", "ClientProfile_Id");
            CreateIndex("dbo.Services", "ProviderProfile_Id");
            AddForeignKey("dbo.Feedbacks", "ClientProfileId", "dbo.ClientProfiles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "ExecutorProfileId", "dbo.ClientProfiles", "Id");
            AddForeignKey("dbo.Services", "ProviderProfile_Id", "dbo.BaseProfiles", "Id");
            AddForeignKey("dbo.Orders", "ClientProfile_Id", "dbo.ClientProfiles", "Id");
            AddForeignKey("dbo.Feedbacks", "ProviderProfile_Id", "dbo.BaseProfiles", "Id");
            DropColumn("dbo.Orders", "ProviderProfileId");
            DropTable("dbo.ProviderServiceses");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProviderServiceses",
                c => new
                    {
                        ProviderProfileId = c.String(nullable: false, maxLength: 128),
                        ServiceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProviderProfileId, t.ServiceId });
            
            AddColumn("dbo.Orders", "ProviderProfileId", c => c.String(maxLength: 128));
            DropForeignKey("dbo.Feedbacks", "ProviderProfile_Id", "dbo.BaseProfiles");
            DropForeignKey("dbo.Orders", "ClientProfile_Id", "dbo.ClientProfiles");
            DropForeignKey("dbo.Services", "ProviderProfile_Id", "dbo.BaseProfiles");
            DropForeignKey("dbo.ProviderServices", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.ProviderServices", "ClientProfileId", "dbo.ClientProfiles");
            DropForeignKey("dbo.Orders", "ExecutorProfileId", "dbo.ClientProfiles");
            DropForeignKey("dbo.Feedbacks", "ClientProfileId", "dbo.ClientProfiles");
            DropIndex("dbo.ProviderServices", new[] { "ServiceId" });
            DropIndex("dbo.ProviderServices", new[] { "ClientProfileId" });
            DropIndex("dbo.Services", new[] { "ProviderProfile_Id" });
            DropIndex("dbo.Orders", new[] { "ClientProfile_Id" });
            DropIndex("dbo.Orders", new[] { "ExecutorProfileId" });
            DropIndex("dbo.Feedbacks", new[] { "ProviderProfile_Id" });
            DropIndex("dbo.Feedbacks", new[] { "ClientProfileId" });
            DropIndex("dbo.ClientProfiles", new[] { "Id" });
            AlterColumn("dbo.Feedbacks", "ProviderProfile_Id", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Services", "ProviderProfile_Id");
            DropColumn("dbo.Feedbacks", "ClientProfileId");
            DropColumn("dbo.Orders", "ClientProfile_Id");
            DropColumn("dbo.Orders", "ExecutorProfileId");
            DropTable("dbo.ProviderServices");
            DropTable("dbo.ClientProfiles");
            RenameColumn(table: "dbo.Feedbacks", name: "ProviderProfile_Id", newName: "ProviderProfileId");
            CreateIndex("dbo.ProviderServiceses", "ServiceId");
            CreateIndex("dbo.ProviderServiceses", "ProviderProfileId");
            CreateIndex("dbo.Feedbacks", "ProviderProfileId");
            CreateIndex("dbo.Orders", "ProviderProfileId");
            AddForeignKey("dbo.Feedbacks", "ProviderProfileId", "dbo.BaseProfiles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "ClientProfileId", "dbo.BaseProfiles", "Id");
            AddForeignKey("dbo.Orders", "ProviderProfileId", "dbo.BaseProfiles", "Id");
            AddForeignKey("dbo.ProviderServiceses", "ServiceId", "dbo.Services", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProviderServiceses", "ProviderProfileId", "dbo.BaseProfiles", "Id", cascadeDelete: true);
        }
    }
}
