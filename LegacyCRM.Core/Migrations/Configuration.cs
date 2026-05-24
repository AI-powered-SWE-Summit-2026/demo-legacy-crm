using System;
using System.Data.Entity.Migrations;
using System.Linq;
using LegacyCRM.Core.Models;

namespace LegacyCRM.Core.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<LegacyCRM.Core.CrmDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "LegacyCRM.Core.CrmDbContext";
        }

        protected override void Seed(LegacyCRM.Core.CrmDbContext context)
        {
            context.Users.AddOrUpdate(
                u => u.Username,
                new User
                {
                    Username = "admin",
                    PasswordHash = "admin123",
                    FullName = "System Administrator",
                    Email = "admin@legacycrm.local",
                    Role = "Administrator",
                    IsActive = true,
                    LastLoginAt = DateTime.UtcNow
                },
                new User
                {
                    Username = "salesrep",
                    PasswordHash = "sales123",
                    FullName = "Sales Representative",
                    Email = "salesrep@legacycrm.local",
                    Role = "Sales",
                    IsActive = true,
                    LastLoginAt = DateTime.UtcNow.AddDays(-1)
                });
            context.SaveChanges();

            var admin = context.Users.Single(u => u.Username == "admin");
            var salesRep = context.Users.Single(u => u.Username == "salesrep");

            context.Customers.AddOrUpdate(
                c => c.Email,
                new Customer
                {
                    Name = "Ava Thompson",
                    CompanyName = "Northwind Traders",
                    Email = "ava@northwind.example",
                    Phone = "555-0101",
                    Address = "100 Market Street",
                    City = "Seattle",
                    Country = "USA",
                    Industry = "Retail",
                    Status = "Active",
                    AssignedToUserId = salesRep.Id,
                    CreatedAt = DateTime.UtcNow.AddDays(-60),
                    ModifiedAt = DateTime.UtcNow.AddDays(-2),
                    Notes = "Seeded default customer."
                },
                new Customer
                {
                    Name = "Liam Carter",
                    CompanyName = "Contoso Logistics",
                    Email = "liam@contoso.example",
                    Phone = "555-0102",
                    Address = "22 Harbor Way",
                    City = "Hamburg",
                    Country = "Germany",
                    Industry = "Logistics",
                    Status = "Prospect",
                    AssignedToUserId = salesRep.Id,
                    CreatedAt = DateTime.UtcNow.AddDays(-30),
                    ModifiedAt = DateTime.UtcNow.AddDays(-1),
                    Notes = "Seeded prospect."
                });
            context.SaveChanges();

            var northwind = context.Customers.Single(c => c.Email == "ava@northwind.example");
            var contoso = context.Customers.Single(c => c.Email == "liam@contoso.example");

            context.Contacts.AddOrUpdate(
                c => c.Email,
                new Contact
                {
                    CustomerId = northwind.Id,
                    FirstName = "Ava",
                    LastName = "Thompson",
                    Email = "ava@northwind.example",
                    Phone = "555-0101",
                    Title = "Operations Director",
                    IsPrimary = true,
                    CreatedAt = DateTime.UtcNow.AddDays(-55)
                },
                new Contact
                {
                    CustomerId = contoso.Id,
                    FirstName = "Liam",
                    LastName = "Carter",
                    Email = "liam@contoso.example",
                    Phone = "555-0102",
                    Title = "VP Sales",
                    IsPrimary = true,
                    CreatedAt = DateTime.UtcNow.AddDays(-28)
                });
            context.SaveChanges();

            context.Opportunities.AddOrUpdate(
                o => o.Title,
                new Opportunity
                {
                    CustomerId = northwind.Id,
                    Title = "Northwind Renewal",
                    Description = "Annual renewal and seat expansion.",
                    Value = 125000m,
                    Stage = "Negotiation",
                    ExpectedCloseDate = DateTime.UtcNow.AddDays(30),
                    AssignedToUserId = salesRep.Id,
                    Probability = 80,
                    CreatedAt = DateTime.UtcNow.AddDays(-14),
                    ModifiedAt = DateTime.UtcNow.AddDays(-1)
                },
                new Opportunity
                {
                    CustomerId = contoso.Id,
                    Title = "Contoso Fleet Visibility",
                    Description = "Initial CRM rollout for logistics team.",
                    Value = 68000m,
                    Stage = "Proposal",
                    ExpectedCloseDate = DateTime.UtcNow.AddDays(45),
                    AssignedToUserId = admin.Id,
                    Probability = 60,
                    CreatedAt = DateTime.UtcNow.AddDays(-10),
                    ModifiedAt = DateTime.UtcNow.AddDays(-2)
                });
            context.SaveChanges();

            context.Tickets.AddOrUpdate(
                t => t.Title,
                new Ticket
                {
                    CustomerId = northwind.Id,
                    Title = "Import job failure",
                    Description = "Nightly import fails on duplicate rows.",
                    Priority = "High",
                    Status = "Open",
                    AssignedToUserId = admin.Id,
                    CreatedAt = DateTime.UtcNow.AddDays(-3)
                });
            context.SaveChanges();

            var opportunity = context.Opportunities.Single(o => o.Title == "Northwind Renewal");
            var contact = context.Contacts.Single(c => c.Email == "ava@northwind.example");

            context.Activities.AddOrUpdate(
                a => a.Description,
                new Activity
                {
                    CustomerId = northwind.Id,
                    ContactId = contact.Id,
                    OpportunityId = opportunity.Id,
                    Type = "Call",
                    Description = "Seeded renewal kickoff call.",
                    ActivityDate = DateTime.UtcNow.AddDays(-5),
                    CreatedByUserId = salesRep.Id
                });
        }
    }
}
