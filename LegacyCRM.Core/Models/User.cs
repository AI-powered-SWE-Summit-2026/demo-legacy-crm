using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LegacyCRM.Core.Models
{
    public class User
    {
        public User()
        {
            AssignedCustomers = new HashSet<Customer>();
            AssignedOpportunities = new HashSet<Opportunity>();
            AssignedTickets = new HashSet<Ticket>();
            Activities = new HashSet<Activity>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Username { get; set; }

        [Required]
        [StringLength(256)]
        public string PasswordHash { get; set; }

        [Required]
        [StringLength(150)]
        public string FullName { get; set; }

        [Required]
        [StringLength(200)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Role { get; set; }

        public bool IsActive { get; set; }

        public DateTime? LastLoginAt { get; set; }

        public virtual ICollection<Customer> AssignedCustomers { get; set; }

        public virtual ICollection<Opportunity> AssignedOpportunities { get; set; }

        public virtual ICollection<Ticket> AssignedTickets { get; set; }

        public virtual ICollection<Activity> Activities { get; set; }
    }
}
