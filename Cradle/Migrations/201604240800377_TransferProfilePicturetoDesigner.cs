namespace Cradle.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TransferProfilePicturetoDesigner : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PersonalProfile", "ProfilePicture_ImageID", "dbo.Image");
            DropIndex("dbo.PersonalProfile", new[] { "ProfilePicture_ImageID" });
            AddColumn("dbo.DesignerProfile", "ProfilePicture_ImageID", c => c.Int());
            CreateIndex("dbo.DesignerProfile", "ProfilePicture_ImageID");
            AddForeignKey("dbo.DesignerProfile", "ProfilePicture_ImageID", "dbo.Image", "ImageID");
            DropColumn("dbo.PersonalProfile", "ProfilePicture_ImageID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PersonalProfile", "ProfilePicture_ImageID", c => c.Int());
            DropForeignKey("dbo.DesignerProfile", "ProfilePicture_ImageID", "dbo.Image");
            DropIndex("dbo.DesignerProfile", new[] { "ProfilePicture_ImageID" });
            DropColumn("dbo.DesignerProfile", "ProfilePicture_ImageID");
            CreateIndex("dbo.PersonalProfile", "ProfilePicture_ImageID");
            AddForeignKey("dbo.PersonalProfile", "ProfilePicture_ImageID", "dbo.Image", "ImageID");
        }
    }
}
