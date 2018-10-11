namespace EventManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class visitor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VisitorRegistrations", "CreatedBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.VisitorRegistrations", "CreatedBy");
        }
    }
}
