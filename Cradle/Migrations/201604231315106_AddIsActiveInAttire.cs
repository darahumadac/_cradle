namespace Cradle.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsActiveInAttire : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DesignerAttire", "IsSelected", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DesignerAttire", "IsSelected");
        }
    }
}
