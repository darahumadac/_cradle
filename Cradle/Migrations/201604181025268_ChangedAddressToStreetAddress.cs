namespace Cradle.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedAddressToStreetAddress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Address", "StreetAddress", c => c.String());
            DropColumn("dbo.Address", "StreetNo");
            DropColumn("dbo.Address", "StreetName");
            DropColumn("dbo.Address", "Municipality");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Address", "Municipality", c => c.String());
            AddColumn("dbo.Address", "StreetName", c => c.String());
            AddColumn("dbo.Address", "StreetNo", c => c.String());
            DropColumn("dbo.Address", "StreetAddress");
        }
    }
}
