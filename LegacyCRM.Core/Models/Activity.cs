using System;
using System.ComponentModel.DataAnnotations;

namespace LegacyCRM.Core.Models
{
    public class Activity
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int? ContactId { get; set; }

        public int? OpportunityId { get; set; }

        [Required]
        [StringLength(50)]
        public string Type { get; set; }

        public string Description { get; set; }

        public DateTime ActivityDate { get; set; }

        public int CreatedByUserId { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Contact Contact { get; set; }

        public virtual Opportunity Opportunity { get; set; }

        public virtual User CreatedByUser { get; set; }
    }
}
