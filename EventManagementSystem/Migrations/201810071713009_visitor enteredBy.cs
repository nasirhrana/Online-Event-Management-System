namespace EventManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class visitorenteredBy : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VisitorRegistrations", "EnteredBy", c => c.String());
            DropColumn("dbo.VisitorRegistrations", "CreatedBy");
        }
        
        public override void Down()
        {
            AddColumn("dbo.VisitorRegistrations", "CreatedBy", c => c.String());
            DropColumn("dbo.VisitorRegistrations", "EnteredBy");
        }
    }
}
