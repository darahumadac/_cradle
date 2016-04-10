namespace Cradle.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSecurityQuestions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SecurityQuestions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SecurityQuestion = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SecurityQuestions");
        }
    }
}
