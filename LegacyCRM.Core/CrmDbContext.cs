using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using LegacyCRM.Core.Models;

namespace LegacyCRM.Core
{
    public class CrmDbContext : DbContext
    {
        static CrmDbContext()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<CrmDbContext>());
        }

        public CrmDbContext()
            : base("name=LegacyCRMDb")
        {
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Opportunity> Opportunities { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Activity> Activities { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<EmailLog> EmailLogs { get; set; }

        public ObjectContext GetObjectContext()
        {
            return ((IObjectContextAdapter)this).ObjectContext;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("Customers");
            modelBuilder.Entity<Contact>().ToTable("Contacts");
            modelBuilder.Entity<Opportunity>().ToTable("Opportunities");
            modelBuilder.Entity<Ticket>().ToTable("Tickets");
            modelBuilder.Entity<Activity>().ToTable("Activities");
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<EmailLog>().ToTable("EmailLog");

            modelBuilder.Entity<Customer>()
                .HasOptional(c => c.AssignedUser)
                .WithMany(u => u.AssignedCustomers)
                .HasForeignKey(c => c.AssignedToUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Contact>()
                .HasRequired(c => c.Customer)
                .WithMany(c => c.Contacts)
                .HasForeignKey(c => c.CustomerId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Opportunity>()
                .Property(o => o.Value)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Opportunity>()
                .HasRequired(o => o.Customer)
                .WithMany(c => c.Opportunities)
                .HasForeignKey(o => o.CustomerId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Opportunity>()
                .HasRequired(o => o.AssignedUser)
                .WithMany(u => u.AssignedOpportunities)
                .HasForeignKey(o => o.AssignedToUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ticket>()
                .HasRequired(t => t.Customer)
                .WithMany(c => c.Tickets)
                .HasForeignKey(t => t.CustomerId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Ticket>()
                .HasRequired(t => t.AssignedUser)
                .WithMany(u => u.AssignedTickets)
                .HasForeignKey(t => t.AssignedToUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Activity>()
                .HasRequired(a => a.Customer)
                .WithMany(c => c.Activities)
                .HasForeignKey(a => a.CustomerId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Activity>()
                .HasOptional(a => a.Contact)
                .WithMany(c => c.Activities)
                .HasForeignKey(a => a.ContactId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Activity>()
                .HasOptional(a => a.Opportunity)
                .WithMany(o => o.Activities)
                .HasForeignKey(a => a.OpportunityId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Activity>()
                .HasRequired(a => a.CreatedByUser)
                .WithMany(u => u.Activities)
                .HasForeignKey(a => a.CreatedByUserId)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
