using System.ComponentModel.DataAnnotations;

namespace MMS_ADP631_FA1.Models
{
    public class Citizen
    {
        [Key]
        public int CitizenID {  get; set; } // Primary Key
        
        [Required]
        public required string FullName {  get; set; }

        [Required]
        public required string Address { get; set; }

        [Required]
        public required string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        public ICollection<ServiceRequest>? ServiceRequests { get; set; }

        public ICollection<Report>? Reports { get; set; }
    }
}
