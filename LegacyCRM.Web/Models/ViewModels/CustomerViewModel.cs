using System.ComponentModel.DataAnnotations;

namespace LegacyCRM.Web.Models.ViewModels
{
    public class CustomerViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Primary Contact")]
        [StringLength(150)]
        public string Name { get; set; }

        [Display(Name = "Company")]
        [StringLength(150)]
        public string CompanyName { get; set; }

        [Required]
        [EmailAddress]
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

        [Display(Name = "Assigned User")]
        public int? AssignedToUserId { get; set; }

        public string Notes { get; set; }
    }
}
