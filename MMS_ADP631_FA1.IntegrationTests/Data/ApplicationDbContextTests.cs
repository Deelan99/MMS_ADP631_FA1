using Microsoft.EntityFrameworkCore;
using MMS_ADP631_FA1.Data;
using MMS_ADP631_FA1.Models;
using Xunit;
using System;
using System.Linq;

namespace MMS_ADP631_FA1.IntegrationTests.Data
{
    public class ApplicationDbContextTests
    {
        private ApplicationDbContext GetDbContext(string? dbName = null)
        {
            dbName ??= Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            return new ApplicationDbContext(options);
        }

        [Fact]
        public void CanInsertCitizen()
        {
            var dbName = nameof(CanInsertCitizen);

            using (var context = GetDbContext(dbName))
            {
                var citizen = new Citizen
                {
                    FullName = "Johnny Doe",
                    Address = "55 Field St",
                    PhoneNumber = "1234567890",
                    Email = "test@thismail.com",
                    DateOfBirth = new DateTime(1985, 12, 8)
                };
                context.Citizens.Add(citizen);
                context.SaveChanges();
            }

            using (var context = GetDbContext(dbName))
            {
                var citizens = context.Citizens.ToList();
                Assert.Single(citizens);
                Assert.Equal("Johnny Doe", citizens.First().FullName);
            }
        }

        [Fact]
        public void CanRetrieveSeededData()
        {
            var dbName = nameof(CanRetrieveSeededData);

            using (var context = GetDbContext(dbName))
            {
                var seededCitizen = new Citizen { CitizenID = 1, FullName = "John Doe", Address = "123 Test Street", PhoneNumber = "123 333 4455", Email = "johnd@testemail.com" };
                context.Citizens.Add(seededCitizen);
                context.SaveChanges();
            }

            using (var context = GetDbContext(dbName))
            {
                var citizen = context.Citizens.Find(1);
                Assert.NotNull(citizen);
                Assert.Equal("John Doe", citizen.FullName);
            }
        }

        [Fact]
        public void CanDeleteCitizen_AndCascadeDeleteServiceRequests()
        {
            var dbName = nameof(CanDeleteCitizen_AndCascadeDeleteServiceRequests);

            using (var context = GetDbContext(dbName))
            {
                var citizen = new Citizen
                {
                    FullName = "Delete Me",
                    Address = "N/A",
                    PhoneNumber = "0000000000",
                    Email = "delete@mail.com"
                };

                var serviceRequest = new ServiceRequest { ServiceType = "Pothole on Main Road", Status = "Pending", Citizen = citizen };

                context.Citizens.Add(citizen);
                context.ServiceRequests.Add(serviceRequest);
                context.SaveChanges();
            }

            using (var context = GetDbContext(dbName))
            {
                var citizen = context.Citizens.Include(c => c.ServiceRequests)
                                              .FirstOrDefault(c => c.FullName == "Delete Me");

                Assert.NotNull(citizen);
                Assert.Single(citizen.ServiceRequests);

                context.Citizens.Remove(citizen);
                context.SaveChanges();
            }

            using (var context = GetDbContext(dbName))
            {
                Assert.Empty(context.Citizens);
                Assert.Empty(context.ServiceRequests);
            }
        }

        [Fact]
        public void CanUpdateReportStatus()
        {
            var dbName = nameof(CanUpdateReportStatus);

            using (var context = GetDbContext(dbName))
            {
                var staff = new Staff
                {
                    FullName = "George Lucas",
                    Position = "Visionary",
                    Department = "Main",
                    Email = "GeorgeL@email.com",
                    PhoneNumber = "123 333 4455"
                };

                var report = new Report { ReportID = 1, ReportType = "Damage To Property", Details = "Report Details", Status = "Pending", StaffID = staff.StaffID };
                context.Reports.Add(report);
                context.SaveChanges();
            }

            using (var context = GetDbContext(dbName))
            {
                var report = context.Reports.Find(1);
                Assert.NotNull(report);
                report.Status = "Approved";
                context.SaveChanges();
            }

            using (var context = GetDbContext(dbName))
            {
                var updatedReport = context.Reports.Find(1);
                Assert.Equal("Approved", updatedReport?.Status);
            }
        }
    }
}
