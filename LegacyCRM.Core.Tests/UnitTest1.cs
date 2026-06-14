using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LegacyCRM.Core.Models;
using Xunit;

namespace LegacyCRM.Core.Tests
{
    public class CoreModelTests
    {
        [Fact]
        public void Customer_Constructor_InitializesChildCollections()
        {
            var customer = new Customer();

            Assert.NotNull(customer.Contacts);
            Assert.NotNull(customer.Opportunities);
            Assert.NotNull(customer.Tickets);
            Assert.NotNull(customer.Activities);
            Assert.Empty(customer.Contacts);
            Assert.Empty(customer.Opportunities);
            Assert.Empty(customer.Tickets);
            Assert.Empty(customer.Activities);
        }

        [Fact]
        public void User_Constructor_InitializesAssignmentCollections()
        {
            var user = new User();

            Assert.NotNull(user.AssignedCustomers);
            Assert.NotNull(user.AssignedOpportunities);
            Assert.NotNull(user.AssignedTickets);
            Assert.NotNull(user.Activities);
        }

        [Fact]
        public void Contact_Constructor_InitializesActivitiesCollection()
        {
            var contact = new Contact();

            Assert.NotNull(contact.Activities);
            Assert.Empty(contact.Activities);
        }

        [Fact]
        public void Opportunity_Constructor_InitializesActivitiesCollection()
        {
            var opportunity = new Opportunity();

            Assert.NotNull(opportunity.Activities);
            Assert.Empty(opportunity.Activities);
        }

        [Fact]
        public void Customer_Validation_Fails_WhenRequiredFieldsAreMissing()
        {
            var customer = new Customer();

            var errors = Validate(customer);

            Assert.Contains(errors, e => e.MemberNames.Contains(nameof(Customer.Name)));
            Assert.Contains(errors, e => e.MemberNames.Contains(nameof(Customer.Email)));
            Assert.Contains(errors, e => e.MemberNames.Contains(nameof(Customer.Status)));
        }

        [Fact]
        public void Customer_Validation_Fails_WhenNameExceedsLimit()
        {
            var customer = new Customer
            {
                Name = new string('N', 151),
                Email = "valid@example.com",
                Status = "Active"
            };

            var errors = Validate(customer);

            Assert.Contains(errors, e => e.MemberNames.Contains(nameof(Customer.Name)));
        }

        [Fact]
        public void Ticket_Validation_Fails_WhenPriorityAndStatusMissing()
        {
            var ticket = new Ticket
            {
                Title = "Support needed"
            };

            var errors = Validate(ticket);

            Assert.Contains(errors, e => e.MemberNames.Contains(nameof(Ticket.Priority)));
            Assert.Contains(errors, e => e.MemberNames.Contains(nameof(Ticket.Status)));
        }

        [Fact]
        public void EmailLog_Validation_Fails_WhenMandatoryFieldsMissing()
        {
            var emailLog = new EmailLog();

            var errors = Validate(emailLog);

            Assert.Contains(errors, e => e.MemberNames.Contains(nameof(EmailLog.ToAddress)));
            Assert.Contains(errors, e => e.MemberNames.Contains(nameof(EmailLog.Subject)));
            Assert.Contains(errors, e => e.MemberNames.Contains(nameof(EmailLog.Body)));
            Assert.Contains(errors, e => e.MemberNames.Contains(nameof(EmailLog.Status)));
        }

        [Fact]
        public void User_Validation_Passes_WhenRequiredFieldsProvided()
        {
            var user = new User
            {
                Username = "salesrep",
                PasswordHash = "hash-value",
                FullName = "Sales Rep",
                Email = "salesrep@example.com",
                Role = "Sales",
                IsActive = true,
                LastLoginAt = DateTime.UtcNow
            };

            var errors = Validate(user);

            Assert.Empty(errors);
        }

        private static List<ValidationResult> Validate(object model)
        {
            var context = new ValidationContext(model, null, null);
            var errors = new List<ValidationResult>();
            Validator.TryValidateObject(model, context, errors, validateAllProperties: true);
            return errors;
        }
    }
}
