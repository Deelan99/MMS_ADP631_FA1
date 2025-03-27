using System.ComponentModel.DataAnnotations;

namespace MMS_ADP631_FA1.Models
{
    public class ServiceRequest
    {
        public int RequestID { get; set; } // Primary Key
        public int CitizenID { get; set; } // Foreign key 

        [Required]
        public string ServiceType { get; set; }
        public DateTime RequestDate { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Pending";

        public Citizen Citizen { get; set; }
    }
}
