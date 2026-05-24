using System.Data.Entity.Migrations;

namespace LegacyCRM.Core.Migrations
{
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 100),
                        PasswordHash = c.String(nullable: false, maxLength: 256),
                        FullName = c.String(nullable: false, maxLength: 150),
                        Email = c.String(nullable: false, maxLength: 200),
                        Role = c.String(nullable: false, maxLength: 50),
                        IsActive = c.Boolean(nullable: false),
                        LastLoginAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        CompanyName = c.String(maxLength: 150),
                        Email = c.String(nullable: false, maxLength: 200),
                        Phone = c.String(maxLength: 50),
                        Address = c.String(maxLength: 250),
                        City = c.String(maxLength: 100),
                        Country = c.String(maxLength: 100),
                        Industry = c.String(maxLength: 100),
                        Status = c.String(nullable: false, maxLength: 50),
                        AssignedToUserId = c.Int(),
                        CreatedAt = c.DateTime(nullable: false),
                        ModifiedAt = c.DateTime(nullable: false),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AssignedToUserId)
                .Index(t => t.AssignedToUserId);

            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        Email = c.String(maxLength: 200),
                        Phone = c.String(maxLength: 50),
                        Title = c.String(maxLength: 100),
                        IsPrimary = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);

            CreateTable(
                "dbo.Opportunities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 150),
                        Description = c.String(),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Stage = c.String(nullable: false, maxLength: 50),
                        ExpectedCloseDate = c.DateTime(nullable: false),
                        AssignedToUserId = c.Int(nullable: false),
                        Probability = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        ModifiedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AssignedToUserId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.AssignedToUserId);

            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 150),
                        Description = c.String(),
                        Priority = c.String(nullable: false, maxLength: 50),
                        Status = c.String(nullable: false, maxLength: 50),
                        AssignedToUserId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        ResolvedAt = c.DateTime(),
                        ResolutionNotes = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AssignedToUserId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.AssignedToUserId);

            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        ContactId = c.Int(),
                        OpportunityId = c.Int(),
                        Type = c.String(nullable: false, maxLength: 50),
                        Description = c.String(),
                        ActivityDate = c.DateTime(nullable: false),
                        CreatedByUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contacts", t => t.ContactId)
                .ForeignKey("dbo.Opportunities", t => t.OpportunityId)
                .ForeignKey("dbo.Users", t => t.CreatedByUserId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.ContactId)
                .Index(t => t.OpportunityId)
                .Index(t => t.CreatedByUserId);

            CreateTable(
                "dbo.EmailLog",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ToAddress = c.String(nullable: false, maxLength: 200),
                        Subject = c.String(nullable: false, maxLength: 200),
                        Body = c.String(nullable: false),
                        Status = c.String(nullable: false, maxLength: 50),
                        SentAt = c.DateTime(nullable: false),
                        ErrorMessage = c.String(),
                    })
                .PrimaryKey(t => t.Id);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Activities", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Activities", "CreatedByUserId", "dbo.Users");
            DropForeignKey("dbo.Activities", "OpportunityId", "dbo.Opportunities");
            DropForeignKey("dbo.Activities", "ContactId", "dbo.Contacts");
            DropForeignKey("dbo.Tickets", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Tickets", "AssignedToUserId", "dbo.Users");
            DropForeignKey("dbo.Opportunities", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Opportunities", "AssignedToUserId", "dbo.Users");
            DropForeignKey("dbo.Contacts", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Customers", "AssignedToUserId", "dbo.Users");
            DropIndex("dbo.Activities", new[] { "CreatedByUserId" });
            DropIndex("dbo.Activities", new[] { "OpportunityId" });
            DropIndex("dbo.Activities", new[] { "ContactId" });
            DropIndex("dbo.Activities", new[] { "CustomerId" });
            DropIndex("dbo.Tickets", new[] { "AssignedToUserId" });
            DropIndex("dbo.Tickets", new[] { "CustomerId" });
            DropIndex("dbo.Opportunities", new[] { "AssignedToUserId" });
            DropIndex("dbo.Opportunities", new[] { "CustomerId" });
            DropIndex("dbo.Contacts", new[] { "CustomerId" });
            DropIndex("dbo.Customers", new[] { "AssignedToUserId" });
            DropTable("dbo.EmailLog");
            DropTable("dbo.Activities");
            DropTable("dbo.Tickets");
            DropTable("dbo.Opportunities");
            DropTable("dbo.Contacts");
            DropTable("dbo.Customers");
            DropTable("dbo.Users");
        }
    }
}
