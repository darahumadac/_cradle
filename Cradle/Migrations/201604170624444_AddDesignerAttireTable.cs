namespace Cradle.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDesignerAttireTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DesignerAttire",
                c => new
                    {
                        DesignerAttireID = c.Int(nullable: false, identity: true),
                        Attire = c.Int(nullable: false),
                        DesignerProfile_DesignerProfileID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.DesignerAttireID)
                .ForeignKey("dbo.DesignerProfile", t => t.DesignerProfile_DesignerProfileID)
                .Index(t => t.DesignerProfile_DesignerProfileID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DesignerAttire", "DesignerProfile_DesignerProfileID", "dbo.DesignerProfile");
            DropIndex("dbo.DesignerAttire", new[] { "DesignerProfile_DesignerProfileID" });
            DropTable("dbo.DesignerAttire");
        }
    }
}
