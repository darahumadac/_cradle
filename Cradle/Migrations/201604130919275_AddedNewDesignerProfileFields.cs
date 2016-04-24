namespace Cradle.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNewDesignerProfileFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DesignerProfile", "IsProfileComplete", c => c.Boolean(nullable: false));
            AddColumn("dbo.DesignerProfile", "Tagline", c => c.String());
            AddColumn("dbo.DesignerProfile", "StyleDescription", c => c.String());
            AddColumn("dbo.DesignerProfile", "IsRTW", c => c.Boolean(nullable: false));
            AddColumn("dbo.DesignerProfile", "IsCustomMade", c => c.Boolean(nullable: false));
            AddColumn("dbo.DesignerProfile", "RTWMinDeliveryDays", c => c.Int(nullable: false));
            AddColumn("dbo.DesignerProfile", "RTWMaxDeliveryDays", c => c.Int(nullable: false));
            AddColumn("dbo.DesignerProfile", "CustomMadeMinDeliveryDays", c => c.Int(nullable: false));
            AddColumn("dbo.DesignerProfile", "CustomMadeMaxDeliveryDays", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DesignerProfile", "CustomMadeMaxDeliveryDays");
            DropColumn("dbo.DesignerProfile", "CustomMadeMinDeliveryDays");
            DropColumn("dbo.DesignerProfile", "RTWMaxDeliveryDays");
            DropColumn("dbo.DesignerProfile", "RTWMinDeliveryDays");
            DropColumn("dbo.DesignerProfile", "IsCustomMade");
            DropColumn("dbo.DesignerProfile", "IsRTW");
            DropColumn("dbo.DesignerProfile", "StyleDescription");
            DropColumn("dbo.DesignerProfile", "Tagline");
            DropColumn("dbo.DesignerProfile", "IsProfileComplete");
        }
    }
}
