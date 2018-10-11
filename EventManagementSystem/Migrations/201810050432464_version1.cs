namespace EventManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class version1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        EventName = c.String(nullable: false),
                        EventVenue = c.String(nullable: false),
                        OrganizerEmail = c.String(nullable: false),
                        ContactNo = c.String(nullable: false),
                        EventDate = c.DateTime(nullable: false),
                        Date = c.String(),
                        StartTime1 = c.Double(nullable: false),
                        StartTime = c.String(),
                        EndTime1 = c.Double(nullable: false),
                        EndTime = c.String(),
                        CreatedBy = c.String(),
                        TimeOfCreation = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.EventId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Email = c.String(nullable: false, maxLength: 50),
                        UserTypeId = c.Int(nullable: false),
                        Password = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserTypes", t => t.UserTypeId, cascadeDelete: true)
                .Index(t => t.Email, unique: true)
                .Index(t => t.UserTypeId);
            
            CreateTable(
                "dbo.UserTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserTypeName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
                        TimeOfRegistration = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.VisitorId)
                .ForeignKey("dbo.Events", t => t.EventId, cascadeDelete: true)
                .Index(t => t.EventId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VisitorRegistrations", "EventId", "dbo.Events");
            DropForeignKey("dbo.Users", "UserTypeId", "dbo.UserTypes");
            DropForeignKey("dbo.Events", "UserId", "dbo.Users");
            DropIndex("dbo.VisitorRegistrations", new[] { "EventId" });
            DropIndex("dbo.Users", new[] { "UserTypeId" });
            DropIndex("dbo.Users", new[] { "Email" });
            DropIndex("dbo.Events", new[] { "UserId" });
            DropTable("dbo.VisitorRegistrations");
            DropTable("dbo.UserTypes");
            DropTable("dbo.Users");
            DropTable("dbo.Events");
        }
    }
}
