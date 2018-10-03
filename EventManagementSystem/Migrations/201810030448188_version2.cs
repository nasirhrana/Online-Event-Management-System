namespace EventManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class version2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VisitorRegistrations",
                c => new
                    {
                        VisitorId = c.Int(nullable: false, identity: true),
                        VisitorName = c.String(nullable: false),
                        VisitorEmail = c.String(nullable: false),
                        VisitorContactNo = c.String(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        DOB = c.String(),
                        Gender = c.String(nullable: false),
                        EventId = c.Int(nullable: false),
                        TicketNo = c.String(nullable: false),
                        TimeOfRegistration = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.VisitorId)
                .ForeignKey("dbo.Events", t => t.EventId, cascadeDelete: true)
                .Index(t => t.EventId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VisitorRegistrations", "EventId", "dbo.Events");
            DropIndex("dbo.VisitorRegistrations", new[] { "EventId" });
            DropTable("dbo.VisitorRegistrations");
        }
    }
}
