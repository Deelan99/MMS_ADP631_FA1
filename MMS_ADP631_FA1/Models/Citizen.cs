using System.ComponentModel.DataAnnotations;

namespace MMS_ADP631_FA1.Models
{
    public class Citizen
    {
        [Key]
        public int CitizenID {  get; set; } // Primary Key
        
        [Required]
        public string? FullName {  get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public DateTime RegistrationDate { get; set; }
        public ICollection<ServiceRequest> ServiceRequests { get; set; } = new List<ServiceRequest>();
    }
}
