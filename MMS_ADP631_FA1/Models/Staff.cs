using System.ComponentModel.DataAnnotations;

namespace MMS_ADP631_FA1.Models
{
    public class Staff
    {
        public int StaffID { get; set; } // Primary Key

        [Required]
        public  string? FullName { get; set; }
        public  string? Position { get; set; }
        public  string? Department { get; set; }

        [Required]
        [EmailAddress]
        public  string? Email { get; set; }
        public  string? PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }

        public ICollection<Report> Reports { get; set; } = new List<Report>();
    }
}
