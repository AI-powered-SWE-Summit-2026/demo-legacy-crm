using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LegacyCRM.Core.Models
{
    public class Customer
    {
        public Customer()
        {
            Contacts = new HashSet<Contact>();
            Opportunities = new HashSet<Opportunity>();
            Tickets = new HashSet<Ticket>();
            Activities = new HashSet<Activity>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [StringLength(150)]
        public string CompanyName { get; set; }

        [Required]
        [StringLength(200)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        [StringLength(250)]
        public string Address { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [StringLength(100)]
        public string Country { get; set; }

        [StringLength(100)]
        public string Industry { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        public int? AssignedToUserId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }

        public string Notes { get; set; }

        public virtual User AssignedUser { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }

        public virtual ICollection<Opportunity> Opportunities { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }

        public virtual ICollection<Activity> Activities { get; set; }
    }
}
