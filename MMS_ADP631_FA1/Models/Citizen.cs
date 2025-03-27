using System.ComponentModel.DataAnnotations;

namespace MMS_ADP631_FA1.Models
{
    public class Citizen
    {
        [Key]
        public int CitizenID {  get; set; } // Primary Key
        
        [Required]
        public string FullName {  get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        public ICollection<ServiceRequest> ServiceRequests { get; set; }

        public ICollection<Report> Reports { get; set; }
    }
}
