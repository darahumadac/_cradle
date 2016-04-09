namespace Cradle.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDatabase_v20 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Address",
                c => new
                    {
                        AddressID = c.Int(nullable: false, identity: true),
                        StreetNo = c.String(),
                        StreetName = c.String(),
                        Municipality = c.String(),
                        City = c.String(),
                        Country = c.String(),
                        ZipCode = c.String(),
                    })
                .PrimaryKey(t => t.AddressID);
            
            CreateTable(
                "dbo.DesignerProfile",
                c => new
                    {
                        DesignerProfileID = c.String(nullable: false, maxLength: 128),
                        BusinessName = c.String(),
                        BusinessEmailAddress = c.String(),
                        Birthdate = c.DateTime(nullable: false),
                        Address_AddressID = c.Int(),
                        ProfileStats_StatisticsID = c.Int(),
                    })
                .PrimaryKey(t => t.DesignerProfileID)
                .ForeignKey("dbo.AspNetUsers", t => t.DesignerProfileID)
                .ForeignKey("dbo.Address", t => t.Address_AddressID)
                .ForeignKey("dbo.Statistics", t => t.ProfileStats_StatisticsID)
                .Index(t => t.DesignerProfileID)
                .Index(t => t.Address_AddressID)
                .Index(t => t.ProfileStats_StatisticsID);
            
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
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        OrderStatus = c.Int(nullable: false),
                        Account_Id = c.String(maxLength: 128),
                        DeliveryDetails_DeliveryID = c.Int(),
                        PaymentDetails_PaymentID = c.Int(),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.AspNetUsers", t => t.Account_Id)
                .ForeignKey("dbo.Delivery", t => t.DeliveryDetails_DeliveryID)
                .ForeignKey("dbo.Payment", t => t.PaymentDetails_PaymentID)
                .Index(t => t.Account_Id)
                .Index(t => t.DeliveryDetails_DeliveryID)
                .Index(t => t.PaymentDetails_PaymentID);
            
            CreateTable(
                "dbo.Delivery",
                c => new
                    {
                        DeliveryID = c.Int(nullable: false, identity: true),
                        DeliveryOptions = c.Int(nullable: false),
                        DeliveryStatus = c.Int(nullable: false),
                        DeliveryAddress_AddressID = c.Int(),
                    })
                .PrimaryKey(t => t.DeliveryID)
                .ForeignKey("dbo.Address", t => t.DeliveryAddress_AddressID)
                .Index(t => t.DeliveryAddress_AddressID);
            
            CreateTable(
                "dbo.OrderItem",
                c => new
                    {
                        OrderItemID = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        ItemOrdered_ItemID = c.Int(),
                        Order_OrderID = c.Int(),
                    })
                .PrimaryKey(t => t.OrderItemID)
                .ForeignKey("dbo.Item", t => t.ItemOrdered_ItemID)
                .ForeignKey("dbo.Order", t => t.Order_OrderID)
                .Index(t => t.ItemOrdered_ItemID)
                .Index(t => t.Order_OrderID);
            
            CreateTable(
                "dbo.Item",
                c => new
                    {
                        ItemID = c.Int(nullable: false, identity: true),
                        ItemType = c.Int(nullable: false),
                        ItemSubtype = c.String(),
                        Material = c.String(),
                        RegularPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DiscountedPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ItemInfo_ProductInformationID = c.Int(),
                        Collection_CollectionID = c.Int(),
                    })
                .PrimaryKey(t => t.ItemID)
                .ForeignKey("dbo.ProductInformation", t => t.ItemInfo_ProductInformationID)
                .ForeignKey("dbo.Collection", t => t.Collection_CollectionID)
                .Index(t => t.ItemInfo_ProductInformationID)
                .Index(t => t.Collection_CollectionID);
            
            CreateTable(
                "dbo.ProductInformation",
                c => new
                    {
                        ProductInformationID = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                        Description = c.String(),
                        ProductStatistics_StatisticsID = c.Int(),
                    })
                .PrimaryKey(t => t.ProductInformationID)
                .ForeignKey("dbo.Statistics", t => t.ProductStatistics_StatisticsID)
                .Index(t => t.ProductStatistics_StatisticsID);
            
            CreateTable(
                "dbo.Statistics",
                c => new
                    {
                        StatisticsID = c.Int(nullable: false, identity: true),
                        LikeCount = c.Int(nullable: false),
                        ViewCount = c.Int(nullable: false),
                        TagCount = c.Int(nullable: false),
                        AveRating = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.StatisticsID);
            
            CreateTable(
                "dbo.Payment",
                c => new
                    {
                        PaymentID = c.Int(nullable: false, identity: true),
                        PaymentOption = c.Int(nullable: false),
                        PaymentStatus = c.Int(nullable: false),
                        PaymentDetails_PaymentDetailID = c.Int(),
                    })
                .PrimaryKey(t => t.PaymentID)
                .ForeignKey("dbo.PaymentDetail", t => t.PaymentDetails_PaymentDetailID)
                .Index(t => t.PaymentDetails_PaymentDetailID);
            
            CreateTable(
                "dbo.PaymentDetail",
                c => new
                    {
                        PaymentDetailID = c.Int(nullable: false, identity: true),
                        AccountName = c.String(),
                        AccountNumber = c.String(),
                    })
                .PrimaryKey(t => t.PaymentDetailID);
            
            CreateTable(
                "dbo.PersonalProfile",
                c => new
                    {
                        PersonalProfileId = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        City = c.String(),
                        Country = c.String(),
                        Birthdate = c.DateTime(nullable: false),
                        Address_AddressID = c.Int(),
                    })
                .PrimaryKey(t => t.PersonalProfileId)
                .ForeignKey("dbo.AspNetUsers", t => t.PersonalProfileId)
                .ForeignKey("dbo.Address", t => t.Address_AddressID)
                .Index(t => t.PersonalProfileId)
                .Index(t => t.Address_AddressID);
            
            CreateTable(
                "dbo.ContactNumber",
                c => new
                    {
                        ContactNumberID = c.Int(nullable: false, identity: true),
                        LandlineNo = c.String(),
                        MobileNo = c.String(),
                        PersonalProfile_PersonalProfileId = c.String(maxLength: 128),
                        DesignerProfile_DesignerProfileID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ContactNumberID)
                .ForeignKey("dbo.PersonalProfile", t => t.PersonalProfile_PersonalProfileId)
                .ForeignKey("dbo.DesignerProfile", t => t.DesignerProfile_DesignerProfileID)
                .Index(t => t.PersonalProfile_PersonalProfileId)
                .Index(t => t.DesignerProfile_DesignerProfileID);
            
            CreateTable(
                "dbo.Collection",
                c => new
                    {
                        CollectionID = c.Int(nullable: false, identity: true),
                        ThemeKeywords = c.String(),
                        MinPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MaxPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CollectionInfo_ProductInformationID = c.Int(),
                        DesignerProfile_DesignerProfileID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.CollectionID)
                .ForeignKey("dbo.ProductInformation", t => t.CollectionInfo_ProductInformationID)
                .ForeignKey("dbo.DesignerProfile", t => t.DesignerProfile_DesignerProfileID)
                .Index(t => t.CollectionInfo_ProductInformationID)
                .Index(t => t.DesignerProfile_DesignerProfileID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DesignerProfile", "ProfileStats_StatisticsID", "dbo.Statistics");
            DropForeignKey("dbo.ContactNumber", "DesignerProfile_DesignerProfileID", "dbo.DesignerProfile");
            DropForeignKey("dbo.Collection", "DesignerProfile_DesignerProfileID", "dbo.DesignerProfile");
            DropForeignKey("dbo.Item", "Collection_CollectionID", "dbo.Collection");
            DropForeignKey("dbo.Collection", "CollectionInfo_ProductInformationID", "dbo.ProductInformation");
            DropForeignKey("dbo.DesignerProfile", "Address_AddressID", "dbo.Address");
            DropForeignKey("dbo.ContactNumber", "PersonalProfile_PersonalProfileId", "dbo.PersonalProfile");
            DropForeignKey("dbo.PersonalProfile", "Address_AddressID", "dbo.Address");
            DropForeignKey("dbo.PersonalProfile", "PersonalProfileId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Order", "PaymentDetails_PaymentID", "dbo.Payment");
            DropForeignKey("dbo.Payment", "PaymentDetails_PaymentDetailID", "dbo.PaymentDetail");
            DropForeignKey("dbo.OrderItem", "Order_OrderID", "dbo.Order");
            DropForeignKey("dbo.OrderItem", "ItemOrdered_ItemID", "dbo.Item");
            DropForeignKey("dbo.Item", "ItemInfo_ProductInformationID", "dbo.ProductInformation");
            DropForeignKey("dbo.ProductInformation", "ProductStatistics_StatisticsID", "dbo.Statistics");
            DropForeignKey("dbo.Order", "DeliveryDetails_DeliveryID", "dbo.Delivery");
            DropForeignKey("dbo.Delivery", "DeliveryAddress_AddressID", "dbo.Address");
            DropForeignKey("dbo.Order", "Account_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.DesignerProfile", "DesignerProfileID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.DesignerProfile", new[] { "ProfileStats_StatisticsID" });
            DropIndex("dbo.ContactNumber", new[] { "DesignerProfile_DesignerProfileID" });
            DropIndex("dbo.Collection", new[] { "DesignerProfile_DesignerProfileID" });
            DropIndex("dbo.Item", new[] { "Collection_CollectionID" });
            DropIndex("dbo.Collection", new[] { "CollectionInfo_ProductInformationID" });
            DropIndex("dbo.DesignerProfile", new[] { "Address_AddressID" });
            DropIndex("dbo.ContactNumber", new[] { "PersonalProfile_PersonalProfileId" });
            DropIndex("dbo.PersonalProfile", new[] { "Address_AddressID" });
            DropIndex("dbo.PersonalProfile", new[] { "PersonalProfileId" });
            DropIndex("dbo.Order", new[] { "PaymentDetails_PaymentID" });
            DropIndex("dbo.Payment", new[] { "PaymentDetails_PaymentDetailID" });
            DropIndex("dbo.OrderItem", new[] { "Order_OrderID" });
            DropIndex("dbo.OrderItem", new[] { "ItemOrdered_ItemID" });
            DropIndex("dbo.Item", new[] { "ItemInfo_ProductInformationID" });
            DropIndex("dbo.ProductInformation", new[] { "ProductStatistics_StatisticsID" });
            DropIndex("dbo.Order", new[] { "DeliveryDetails_DeliveryID" });
            DropIndex("dbo.Delivery", new[] { "DeliveryAddress_AddressID" });
            DropIndex("dbo.Order", new[] { "Account_Id" });
            DropIndex("dbo.DesignerProfile", new[] { "DesignerProfileID" });
            DropIndex("dbo.AspNetUserClaims", new[] { "User_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropTable("dbo.Collection");
            DropTable("dbo.ContactNumber");
            DropTable("dbo.PersonalProfile");
            DropTable("dbo.PaymentDetail");
            DropTable("dbo.Payment");
            DropTable("dbo.Statistics");
            DropTable("dbo.ProductInformation");
            DropTable("dbo.Item");
            DropTable("dbo.OrderItem");
            DropTable("dbo.Delivery");
            DropTable("dbo.Order");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.DesignerProfile");
            DropTable("dbo.Address");
        }
    }
}
