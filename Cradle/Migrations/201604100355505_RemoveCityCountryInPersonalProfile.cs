namespace Cradle.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveCityCountryInPersonalProfile : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PersonalProfile", "City");
            DropColumn("dbo.PersonalProfile", "Country");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PersonalProfile", "Country", c => c.String());
            AddColumn("dbo.PersonalProfile", "City", c => c.String());
        }
    }
}
