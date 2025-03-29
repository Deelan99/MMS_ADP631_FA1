using Microsoft.EntityFrameworkCore;
using MMS_ADP631_FA1.Models;

namespace MMS_ADP631_FA1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Citizen> Citizens { get; set; }
        public DbSet<ServiceRequest> ServiceRequests { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Report> Reports { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Defining Primary Keys
            modelBuilder.Entity<Citizen>().HasKey(c => c.CitizenID);
            modelBuilder.Entity<ServiceRequest>().HasKey(sr => sr.RequestID);
            modelBuilder.Entity<Staff>().HasKey(s => s.StaffID);
            modelBuilder.Entity<Report>().HasKey(r => r.ReportID);

            // Define Foreign key relationships
            modelBuilder.Entity<ServiceRequest>()
                .HasOne(sr => sr.Citizen)
                .WithMany(c => c.ServiceRequests)
                .HasForeignKey(sr => sr.CitizenID);

            modelBuilder.Entity<Report>()
                .HasOne(r => r.Citizen)
                .WithMany(r => r.Reports)
                .HasForeignKey(r => r.CitizenID);

            #region Seed Data
            // Seed Citizens
            modelBuilder.Entity<Citizen>().HasData(
                new Citizen { CitizenID = 1, FullName = "Zara Windcroft", Address = "84 Foggy Hollow Rd", PhoneNumber = "5554321987", Email = "zara.windcroft@mail.com", DateOfBirth = new DateTime(1993, 3, 15) },
                new Citizen { CitizenID = 2, FullName = "Elton Braverman", Address = "22 Maple Creek", PhoneNumber = "5559872345", Email = "elton.bravo@mail.com", DateOfBirth = new DateTime(1987, 11, 2) },
                new Citizen { CitizenID = 3, FullName = "Lyra Montclair", Address = "310 Sunset Boulevard", PhoneNumber = "5551239087", Email = "lyra.monty@mail.com", DateOfBirth = new DateTime(1995, 7, 24) },
                new Citizen { CitizenID = 4, FullName = "Dexter Flannigan", Address = "15 Oakridge Lane", PhoneNumber = "5556781234", Email = "dexter.flan@mail.com", DateOfBirth = new DateTime(1990, 4, 8) }
            );

            // Seed Service Requests
            modelBuilder.Entity<ServiceRequest>().HasData(
                new ServiceRequest { RequestID = 1, CitizenID = 1, ServiceType = "Street Light Repair", Status = "Pending" },
                new ServiceRequest { RequestID = 2, CitizenID = 2, ServiceType = "Garbage Collection Delay", Status = "Completed" },
                new ServiceRequest { RequestID = 3, CitizenID = 3, ServiceType = "Water Pressure Issue", Status = "In Progress" },
                new ServiceRequest { RequestID = 4, CitizenID = 4, ServiceType = "Road Pothole Repair", Status = "Pending" }
            );

            // Seed Staff
            modelBuilder.Entity<Staff>().HasData(
                new Staff { StaffID = 1, FullName = "Marcel Thornby", Position = "Chief Engineer", Department = "Infrastructure", Email = "marcel.thornby@municipality.com", PhoneNumber = "5556549821", HireDate = new DateTime(2015, 5, 12) },
                new Staff { StaffID = 2, FullName = "Lila Hawthorne", Position = "Public Relations Officer", Department = "Communications", Email = "lila.hawthorne@municipality.com", PhoneNumber = "5558912345", HireDate = new DateTime(2018, 9, 3) },
                new Staff { StaffID = 3, FullName = "Eugene Whitlock", Position = "Finance Manager", Department = "Finance", Email = "eugene.whitlock@municipality.com", PhoneNumber = "5557612984", HireDate = new DateTime(2012, 1, 20) }
            );

            // Seed Reports
            modelBuilder.Entity<Report>().HasData(
                new Report { ReportID = 1, CitizenID = 1, ReportType = "Complaint", Details = "Broken street light on Foggy Hollow Rd", Status = "Under Review" },
                new Report { ReportID = 2, CitizenID = 3, ReportType = "Incident Report", Details = "Water overflow at Sunset Boulevard", Status = "Resolved" },
                new Report { ReportID = 3, CitizenID = 4, ReportType = "Feedback", Details = "Quick response on pothole repair", Status = "Closed" }
            );
            #endregion
        }
    }
}