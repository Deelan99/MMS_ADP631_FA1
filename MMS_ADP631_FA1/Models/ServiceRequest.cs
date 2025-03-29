using System.ComponentModel.DataAnnotations;

namespace MMS_ADP631_FA1.Models
{
    public class ServiceRequest
    {
        [Key]
        public int RequestID { get; set; } // Primary Key
        public int CitizenID { get; set; } // Foreign key 

        [Required]
        public required string ServiceType { get; set; }
        public DateTime RequestDate { get; set; }
        public string Status { get; set; } = "Pending";

        public Citizen Citizen { get; set; }
    }
}
