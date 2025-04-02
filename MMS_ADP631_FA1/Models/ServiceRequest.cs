using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace MMS_ADP631_FA1.Models
{
    public class ServiceRequest
    {
        [Key]
        public int RequestID { get; set; } // Primary Key
        public int CitizenID { get; set; } // Foreign key 

        [Required]
        public string? ServiceType { get; set; }
        public DateTime RequestDate { get; set; }
        public string Status { get; set; } = "Pending";

        [BindNever]
        public Citizen? Citizen { get; set; }
    }
}
