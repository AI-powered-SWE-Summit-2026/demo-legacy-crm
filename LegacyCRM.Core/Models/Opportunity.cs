using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LegacyCRM.Core.Models
{
    public class Opportunity
    {
        public Opportunity()
        {
            Activities = new HashSet<Activity>();
        }

        public int Id { get; set; }

        public int CustomerId { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Value { get; set; }

        [Required]
        [StringLength(50)]
        public string Stage { get; set; }

        public DateTime ExpectedCloseDate { get; set; }

        public int AssignedToUserId { get; set; }

        public int Probability { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual User AssignedUser { get; set; }

        public virtual ICollection<Activity> Activities { get; set; }
    }
}
