using System;
using System.ComponentModel.DataAnnotations;

namespace LegacyCRM.Core.Models
{
    public class EmailLog
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string ToAddress { get; set; }

        [Required]
        [StringLength(200)]
        public string Subject { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        public DateTime SentAt { get; set; }

        public string ErrorMessage { get; set; }
    }
}
