using System.ComponentModel.DataAnnotations;

namespace LegacyCRM.Web.Models.ViewModels
{
    public class TicketViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Customer")]
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

        [Required]
        [Display(Name = "Assigned User")]
        public int AssignedToUserId { get; set; }

        [Display(Name = "Resolution Notes")]
        public string ResolutionNotes { get; set; }
    }
}
