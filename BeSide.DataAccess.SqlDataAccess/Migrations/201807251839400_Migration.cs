namespace BeSide.DataAccess.SqlDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.BaseProfiles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Patronymic = c.String(nullable: false, maxLength: 50),
                        ApplicationUserId = c.Int(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ShortDescription = c.String(nullable: false, maxLength: 100),
                        FullDescription = c.String(nullable: false, maxLength: 1000),
                        NameProvider = c.String(maxLength: 40),
                        Price = c.Int(),
                        ContractPrice = c.Boolean(nullable: false),
                        Deadline = c.DateTime(),
                        PhoneNumber = c.String(maxLength: 15),
                        City = c.String(maxLength: 40),
                        ProviderServicesId = c.Int(nullable: false),
                        ClientProfileId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BaseProfiles", t => t.ClientProfileId)
                .ForeignKey("dbo.ProviderServices", t => t.ProviderServicesId, cascadeDelete: true)
                .Index(t => t.ProviderServicesId)
                .Index(t => t.ClientProfileId);
            
            CreateTable(
                "dbo.Feedbacks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProviderProfileId = c.String(nullable: false, maxLength: 128),
                        OrderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.BaseProfiles", t => t.ProviderProfileId, cascadeDelete: true)
                .Index(t => t.ProviderProfileId)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.ProviderServices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProviderProfileId = c.String(nullable: false, maxLength: 128),
                        ServiceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BaseProfiles", t => t.ProviderProfileId, cascadeDelete: true)
                .ForeignKey("dbo.Services", t => t.ServiceId, cascadeDelete: true)
                .Index(t => t.ProviderProfileId)
                .Index(t => t.ServiceId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.ProviderProfileServices",
                c => new
                    {
                        ProviderProfile_Id = c.String(nullable: false, maxLength: 128),
                        Service_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProviderProfile_Id, t.Service_Id })
                .ForeignKey("dbo.BaseProfiles", t => t.ProviderProfile_Id, cascadeDelete: true)
                .ForeignKey("dbo.Services", t => t.Service_Id, cascadeDelete: true)
                .Index(t => t.ProviderProfile_Id)
                .Index(t => t.Service_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ProviderProfileServices", "Service_Id", "dbo.Services");
            DropForeignKey("dbo.ProviderProfileServices", "ProviderProfile_Id", "dbo.BaseProfiles");
            DropForeignKey("dbo.Orders", "ProviderServicesId", "dbo.ProviderServices");
            DropForeignKey("dbo.ProviderServices", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.ProviderServices", "ProviderProfileId", "dbo.BaseProfiles");
            DropForeignKey("dbo.Feedbacks", "ProviderProfileId", "dbo.BaseProfiles");
            DropForeignKey("dbo.Feedbacks", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "ClientProfileId", "dbo.BaseProfiles");
            DropForeignKey("dbo.BaseProfiles", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Services", "CategoryId", "dbo.Categories");
            DropIndex("dbo.ProviderProfileServices", new[] { "Service_Id" });
            DropIndex("dbo.ProviderProfileServices", new[] { "ProviderProfile_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ProviderServices", new[] { "ServiceId" });
            DropIndex("dbo.ProviderServices", new[] { "ProviderProfileId" });
            DropIndex("dbo.Feedbacks", new[] { "OrderId" });
            DropIndex("dbo.Feedbacks", new[] { "ProviderProfileId" });
            DropIndex("dbo.Orders", new[] { "ClientProfileId" });
            DropIndex("dbo.Orders", new[] { "ProviderServicesId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.BaseProfiles", new[] { "Id" });
            DropIndex("dbo.Services", new[] { "CategoryId" });
            DropTable("dbo.ProviderProfileServices");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.ProviderServices");
            DropTable("dbo.Feedbacks");
            DropTable("dbo.Orders");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.BaseProfiles");
            DropTable("dbo.Services");
            DropTable("dbo.Categories");
        }
    }
}
