using System.ComponentModel.DataAnnotations;

namespace MMS_ADP631_FA1.Models
{
    public class Report
    {
        [Key]
        public int ReportID { get; set; } // Primary Key
        public int StaffID { get; set; } // Foreign Key
        [Required]
        public string ReportType { get; set; }
        [Required]
        public string Details { get; set; }
        public DateTime SubmissionDate { get; set; }
        public string Status { get; set; } = "Under Review";

        public Staff? Staff { get; set; }
    }
}
