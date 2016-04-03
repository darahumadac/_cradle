namespace Cradle.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrdersTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        OrderItem = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        DeliveryID = c.Int(),
                        PaymentID = c.Int(),
                        RegisteredUserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.Deliveries", t => t.DeliveryID)
                .ForeignKey("dbo.Payments", t => t.PaymentID)
                .ForeignKey("dbo.RegisteredUsers", t => t.RegisteredUserID)
                .Index(t => t.DeliveryID)
                .Index(t => t.PaymentID)
                .Index(t => t.RegisteredUserID);
            
            CreateTable(
                "dbo.Deliveries",
                c => new
                    {
                        DeliveryID = c.Int(nullable: false, identity: true),
                        DeliveryOptions = c.Int(nullable: false),
                        DeliveryAddressID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.DeliveryID)
                .ForeignKey("dbo.Addresses", t => t.DeliveryAddressID)
                .Index(t => t.DeliveryAddressID);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        PaymentID = c.Int(nullable: false, identity: true),
                        PaymentOption = c.Int(nullable: false),
                        PaymentDetailID = c.Int(),
                    })
                .PrimaryKey(t => t.PaymentID)
                .ForeignKey("dbo.PaymentDetails", t => t.PaymentDetailID)
                .Index(t => t.PaymentDetailID);
            
            CreateTable(
                "dbo.PaymentDetails",
                c => new
                    {
                        PaymentDetailID = c.Int(nullable: false, identity: true),
                        AccountName = c.String(),
                        AccountNumber = c.String(),
                    })
                .PrimaryKey(t => t.PaymentDetailID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "RegisteredUser_RegisteredUserID", "dbo.RegisteredUsers");
            DropForeignKey("dbo.Orders", "PaymentDetails_PaymentID", "dbo.Payments");
            DropForeignKey("dbo.Payments", "PaymentDetails_PaymentDetailID", "dbo.PaymentDetails");
            DropForeignKey("dbo.Orders", "DeliveryDetails_DeliveryID", "dbo.Deliveries");
            DropForeignKey("dbo.Deliveries", "DeliveryAddress_AddressID", "dbo.Addresses");
            DropIndex("dbo.Orders", new[] { "RegisteredUser_RegisteredUserID" });
            DropIndex("dbo.Orders", new[] { "PaymentDetails_PaymentID" });
            DropIndex("dbo.Payments", new[] { "PaymentDetails_PaymentDetailID" });
            DropIndex("dbo.Orders", new[] { "DeliveryDetails_DeliveryID" });
            DropIndex("dbo.Deliveries", new[] { "DeliveryAddress_AddressID" });
            DropTable("dbo.PaymentDetails");
            DropTable("dbo.Payments");
            DropTable("dbo.Deliveries");
            DropTable("dbo.Orders");
        }
    }
}
