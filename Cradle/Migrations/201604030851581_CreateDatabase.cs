namespace Cradle.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        Role = c.Int(),
                        EmailAddress = c.String(),
                        SecurityQuestion = c.Int(),
                        SecurityAnswer = c.String(),
                        IsActive = c.Boolean(),
                        FailedLoginCount = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.LoginProvider, t.ProviderKey })
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
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.RegisteredUsers",
                c => new
                    {
                        RegisteredUserID = c.String(nullable: false, maxLength: 128),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        PrimaryAddressID = c.String(maxLength: 128),
                        DesignerProfileID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.RegisteredUserID)
                .ForeignKey("dbo.AspNetUsers", t => t.RegisteredUserID)
                .ForeignKey("dbo.Addresses", t => t.PrimaryAddressID)
                .ForeignKey("dbo.DesignerProfiles", t => t.DesignerProfileID)
                .Index(t => t.RegisteredUserID)
                .Index(t => t.PrimaryAddressID)
                .Index(t => t.DesignerProfileID);
            
            CreateTable(
                "dbo.PersonalProfiles",
                c => new
                    {
                        RegisteredUserID = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        City = c.String(),
                        Country = c.String(),
                        Birthdate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RegisteredUserID)
                .ForeignKey("dbo.RegisteredUsers", t => t.RegisteredUserID)
                .Index(t => t.RegisteredUserID);
            
            CreateTable(
                "dbo.ContactNumbers",
                c => new
                    {
                        ContactNumberID = c.Int(nullable: false, identity: true),
                        LandlineNo = c.String(),
                        MobileNo = c.String(),
                        RegisteredUserID = c.String(maxLength: 128),
                        DesignerProfileID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ContactNumberID)
                .ForeignKey("dbo.PersonalProfiles", t => t.RegisteredUserID)
                .ForeignKey("dbo.DesignerProfiles", t => t.DesignerProfileID)
                .Index(t => t.RegisteredUserID)
                .Index(t => t.DesignerProfileID);
            
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressID = c.String(nullable: false, maxLength: 128),
                        StreetNo = c.String(),
                        StreetName = c.String(),
                        Municipality = c.String(),
                        City = c.String(),
                        Country = c.String(),
                        ZipCode = c.String(),
                    })
                .PrimaryKey(t => t.AddressID);
            
            CreateTable(
                "dbo.Collections",
                c => new
                    {
                        CollectionID = c.String(nullable: false, maxLength: 128),
                        ThemeKeywords = c.String(),
                        MinPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MaxPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductInformationID = c.Int(),
                        RegisteredUserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.CollectionID)
                .ForeignKey("dbo.ProductInformations", t => t.ProductInformationID)
                .ForeignKey("dbo.RegisteredUsers", t => t.RegisteredUserID)
                .Index(t => t.ProductInformationID)
                .Index(t => t.RegisteredUserID);
            
            CreateTable(
                "dbo.ProductInformations",
                c => new
                    {
                        ProductInformationID = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                        Description = c.String(),
                        StatisticsID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ProductInformationID)
                .ForeignKey("dbo.Statistics", t => t.StatisticsID)
                .Index(t => t.StatisticsID);
            
            CreateTable(
                "dbo.Statistics",
                c => new
                    {
                        StatisticsID = c.String(nullable: false, maxLength: 128),
                        LikeCount = c.Int(nullable: false),
                        ViewCount = c.Int(nullable: false),
                        TagCount = c.Int(nullable: false),
                        AveRating = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.StatisticsID);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        ItemID = c.Int(nullable: false, identity: true),
                        ItemType = c.Int(nullable: false),
                        ItemSubtype = c.String(),
                        Material = c.String(),
                        RegularPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DiscountedPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CollectionID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ItemID)
                .ForeignKey("dbo.Collections", t => t.CollectionID)
                .Index(t => t.CollectionID);
            
            CreateTable(
                "dbo.DesignerProfiles",
                c => new
                    {
                        DesignerProfileID = c.String(nullable: false, maxLength: 128),
                        BusinessName = c.String(),
                        BusinessEmailAddress = c.String(),
                        Birthdate = c.DateTime(nullable: false),
                        StatisticsID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.DesignerProfileID)
                .ForeignKey("dbo.Statistics", t => t.StatisticsID)
                .Index(t => t.StatisticsID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RegisteredUsers", "DesignerProfile_DesignerProfileID", "dbo.DesignerProfiles");
            DropForeignKey("dbo.DesignerProfiles", "ProfileStats_StatisticsID", "dbo.Statistics");
            DropForeignKey("dbo.ContactNumbers", "DesignerProfile_DesignerProfileID", "dbo.DesignerProfiles");
            DropForeignKey("dbo.Collections", "Designer_RegisteredUserID", "dbo.RegisteredUsers");
            DropForeignKey("dbo.Items", "Collection_CollectionID", "dbo.Collections");
            DropForeignKey("dbo.Collections", "CollectionInfo_ProductInformationID", "dbo.ProductInformations");
            DropForeignKey("dbo.ProductInformations", "ProductStatistics_StatisticsID", "dbo.Statistics");
            DropForeignKey("dbo.RegisteredUsers", "PrimaryAddress_AddressID", "dbo.Addresses");
            DropForeignKey("dbo.PersonalProfiles", "RegisteredUserID", "dbo.RegisteredUsers");
            DropForeignKey("dbo.ContactNumbers", "PersonalProfile_RegisteredUserID", "dbo.PersonalProfiles");
            DropForeignKey("dbo.RegisteredUsers", "RegisteredUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.RegisteredUsers", new[] { "DesignerProfile_DesignerProfileID" });
            DropIndex("dbo.DesignerProfiles", new[] { "ProfileStats_StatisticsID" });
            DropIndex("dbo.ContactNumbers", new[] { "DesignerProfile_DesignerProfileID" });
            DropIndex("dbo.Collections", new[] { "Designer_RegisteredUserID" });
            DropIndex("dbo.Items", new[] { "Collection_CollectionID" });
            DropIndex("dbo.Collections", new[] { "CollectionInfo_ProductInformationID" });
            DropIndex("dbo.ProductInformations", new[] { "ProductStatistics_StatisticsID" });
            DropIndex("dbo.RegisteredUsers", new[] { "PrimaryAddress_AddressID" });
            DropIndex("dbo.PersonalProfiles", new[] { "RegisteredUserID" });
            DropIndex("dbo.ContactNumbers", new[] { "PersonalProfile_RegisteredUserID" });
            DropIndex("dbo.RegisteredUsers", new[] { "RegisteredUserID" });
            DropIndex("dbo.AspNetUserClaims", new[] { "User_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropTable("dbo.DesignerProfiles");
            DropTable("dbo.Items");
            DropTable("dbo.Statistics");
            DropTable("dbo.ProductInformations");
            DropTable("dbo.Collections");
            DropTable("dbo.Addresses");
            DropTable("dbo.ContactNumbers");
            DropTable("dbo.PersonalProfiles");
            DropTable("dbo.RegisteredUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetRoles");
        }
    }
}
