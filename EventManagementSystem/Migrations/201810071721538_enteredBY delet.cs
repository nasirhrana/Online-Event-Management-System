namespace EventManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class enteredBYdelet : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.VisitorRegistrations", "EnteredBy");
        }
        
        public override void Down()
        {
            AddColumn("dbo.VisitorRegistrations", "EnteredBy", c => c.String());
        }
    }
}
