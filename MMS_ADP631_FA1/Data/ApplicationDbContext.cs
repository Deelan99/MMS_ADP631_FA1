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
        }

    }
}