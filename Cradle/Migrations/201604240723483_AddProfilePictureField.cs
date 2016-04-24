namespace Cradle.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProfilePictureField : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Image",
                c => new
                    {
                        ImageID = c.Int(nullable: false, identity: true),
                        Picture = c.Binary(),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ImageID);
            
            AddColumn("dbo.Item", "ItemImage_ImageID", c => c.Int());
            AddColumn("dbo.PersonalProfile", "ProfilePicture_ImageID", c => c.Int());
            CreateIndex("dbo.Item", "ItemImage_ImageID");
            CreateIndex("dbo.PersonalProfile", "ProfilePicture_ImageID");
            AddForeignKey("dbo.Item", "ItemImage_ImageID", "dbo.Image", "ImageID");
            AddForeignKey("dbo.PersonalProfile", "ProfilePicture_ImageID", "dbo.Image", "ImageID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PersonalProfile", "ProfilePicture_ImageID", "dbo.Image");
            DropForeignKey("dbo.Item", "ItemImage_ImageID", "dbo.Image");
            DropIndex("dbo.PersonalProfile", new[] { "ProfilePicture_ImageID" });
            DropIndex("dbo.Item", new[] { "ItemImage_ImageID" });
            DropColumn("dbo.PersonalProfile", "ProfilePicture_ImageID");
            DropColumn("dbo.Item", "ItemImage_ImageID");
            DropTable("dbo.Image");
        }
    }
}
