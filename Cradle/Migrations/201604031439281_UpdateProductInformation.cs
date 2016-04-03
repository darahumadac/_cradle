namespace Cradle.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProductInformation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "ProductInformationID", c => c.Int());
            CreateIndex("dbo.Items", "ProductInformationID");
            AddForeignKey("dbo.Items", "ProductInformationID", "dbo.ProductInformations", "ProductInformationID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "ItemInfo_ProductInformationID", "dbo.ProductInformations");
            DropIndex("dbo.Items", new[] { "ItemInfo_ProductInformationID" });
            DropColumn("dbo.Items", "ItemInfo_ProductInformationID");
        }
    }
}
