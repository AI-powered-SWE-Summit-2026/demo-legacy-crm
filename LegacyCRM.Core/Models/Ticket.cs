using System;
using System.ComponentModel.DataAnnotations;

namespace LegacyCRM.Core.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        [StringLength(50)]
        public string Priority { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        public int AssignedToUserId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? ResolvedAt { get; set; }

        public string ResolutionNotes { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual User AssignedUser { get; set; }
    }
}
