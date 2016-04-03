namespace Cradle.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCollectionTableToDesignerProfile : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Collections", "RegisteredUserID", "dbo.RegisteredUsers");
            DropIndex("dbo.Collections", new[] { "RegisteredUserID" });
            AddColumn("dbo.Collections", "DesignerProfileID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Collections", "DesignerProfileID");
            AddForeignKey("dbo.Collections", "DesignerProfileID", "dbo.DesignerProfiles", "DesignerProfileID");
            DropColumn("dbo.Collections", "RegisteredUserID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Collections", "Designer_RegisteredUserID", c => c.String(maxLength: 128));
            DropForeignKey("dbo.Collections", "DesignerProfile_DesignerProfileID", "dbo.DesignerProfiles");
            DropIndex("dbo.Collections", new[] { "DesignerProfile_DesignerProfileID" });
            DropColumn("dbo.Collections", "DesignerProfile_DesignerProfileID");
            CreateIndex("dbo.Collections", "Designer_RegisteredUserID");
            AddForeignKey("dbo.Collections", "Designer_RegisteredUserID", "dbo.RegisteredUsers", "RegisteredUserID");
        }
    }
}
