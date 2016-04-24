namespace Cradle.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCountryCodeMobileInContactNo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContactNumber", "CountryCodeMobile", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContactNumber", "CountryCodeMobile");
        }
    }
}
