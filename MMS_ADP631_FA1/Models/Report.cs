using System.ComponentModel.DataAnnotations;

namespace MMS_ADP631_FA1.Models
{
    public class Report
    {
        [Key]
        public int ReportID { get; set; } // Primary Key
        public int CitizenID { get; set; } // Foreign Key
        [Required]
        public required string ReportType { get; set; }
        [Required]
        public required string Details { get; set; }
        public DateTime SubmissionDate { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Under Review";

        public Citizen? Citizen { get; set; }

    }
}
