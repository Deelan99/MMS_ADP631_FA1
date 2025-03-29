using System.ComponentModel.DataAnnotations;

namespace MMS_ADP631_FA1.Models
{
    public class Staff
    {
        public int StaffID { get; set; } // Primary Key

        [Required]
        public required string FullName { get; set; }
        [Required]
        public required string Position { get; set; }

        [Required]
        public required string Department { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        public required string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
    }
}
