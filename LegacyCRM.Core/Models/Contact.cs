using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LegacyCRM.Core.Models
{
    public class Contact
    {
        public Contact()
        {
            Activities = new HashSet<Activity>();
        }

        public int Id { get; set; }

        public int CustomerId { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [StringLength(200)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        public bool IsPrimary { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual ICollection<Activity> Activities { get; set; }
    }
}
