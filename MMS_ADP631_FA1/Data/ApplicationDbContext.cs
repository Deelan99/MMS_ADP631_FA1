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
                .HasForeignKey(sr => sr.CitizenID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Report>()
                .HasOne(r => r.Staff)
                .WithMany(r => r.Reports)
                .HasForeignKey(r => r.StaffID)
                .OnDelete(DeleteBehavior.Cascade);

            // Set database-level default date
            modelBuilder.Entity<Citizen>()
                .Property(c => c.RegistrationDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<ServiceRequest>()
                .Property(sr => sr.RequestDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Staff>()
                .Property(st => st.HireDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Report>()
                .Property(r => r.SubmissionDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

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
                new ServiceRequest { RequestID = 1, CitizenID = 1, ServiceType = "Street Light Repair", Status = "Pending", RequestDate = new DateTime(2024, 3, 1) },
                new ServiceRequest { RequestID = 2, CitizenID = 2, ServiceType = "Garbage Collection Delay", Status = "Completed", RequestDate = new DateTime(2024, 2, 15) },
                new ServiceRequest { RequestID = 3, CitizenID = 3, ServiceType = "Water Pressure Issue", Status = "In Progress", RequestDate = new DateTime(2024, 3, 10) },
                new ServiceRequest { RequestID = 4, CitizenID = 4, ServiceType = "Road Pothole Repair", Status = "Pending", RequestDate = new DateTime(2024, 3, 5) }
            );

            // Seed Staff
            modelBuilder.Entity<Staff>().HasData(
                new Staff { StaffID = 1, FullName = "Marcel Thornby", Position = "Chief Engineer", Department = "Infrastructure", Email = "marcel.thornby@municipality.com", PhoneNumber = "5556549821", HireDate = new DateTime(2015, 5, 12) },
                new Staff { StaffID = 2, FullName = "Lila Hawthorne", Position = "Public Relations Officer", Department = "Communications", Email = "lila.hawthorne@municipality.com", PhoneNumber = "5558912345", HireDate = new DateTime(2018, 9, 3) },
                new Staff { StaffID = 3, FullName = "Eugene Whitlock", Position = "Finance Manager", Department = "Finance", Email = "eugene.whitlock@municipality.com", PhoneNumber = "5557612984", HireDate = new DateTime(2012, 1, 20) },
                new Staff { StaffID = 4, FullName = "Nina Calloway", Position = "City Planner", Department = "Urban Development", Email = "nina.calloway@municipality.com", PhoneNumber = "5553426789", HireDate = new DateTime(2019, 6, 14) }
            );

            // Seed Reports
            modelBuilder.Entity<Report>().HasData(
                new Report { ReportID = 1, StaffID = 1, ReportType = "Complaint", Details = "Broken street light on Foggy Hollow Rd", SubmissionDate = new DateTime(2024, 3, 12), Status = "Under Review" },
                new Report { ReportID = 2, StaffID = 2, ReportType = "Incident Report", Details = "Water overflow at Sunset Boulevard", SubmissionDate = new DateTime(2024, 3, 20), Status = "Resolved" },
                new Report { ReportID = 3, StaffID = 3, ReportType = "Financial Audit", Details = "Annual budget review and expenditure analysis", SubmissionDate = new DateTime(2024, 3, 25), Status = "In Progress" },
                new Report { ReportID = 4, StaffID = 4, ReportType = "Urban Development Proposal", Details = "New park and recreation area proposal", SubmissionDate = new DateTime(2024, 3, 28), Status = "Pending" }
            );
            #endregion
        }
    }
}