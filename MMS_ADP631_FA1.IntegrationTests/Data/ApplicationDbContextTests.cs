using Microsoft.EntityFrameworkCore;
using MMS_ADP631_FA1.Data;
using MMS_ADP631_FA1.Models;
using Xunit;

public class ApplicationDbContextTests
{
    private ApplicationDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
            .Options;

        return new ApplicationDbContext(options);
    }

    [Fact]
    public void CanInsertCitizen()
    {
        using (var context = GetDbContext())
        {
            var citizen = new Citizen { FullName = "Johnny Doe", Address = "55 Field St", PhoneNumber = "1234567890", Email = "test@thismail.com", DateOfBirth = new DateTime(1985, 12, 8) };
            context.Citizens.Add(citizen);
            context.SaveChanges();
        }

        using (var context = GetDbContext())
        {
            Assert.Single(context.Citizens);
            Assert.Equal("Johnny Doe", context.Citizens.First().FullName);
        }
    }

    [Fact]
    public void CanRetrieveSeededData()
    {
        using (var context = GetDbContext())
        {
            var seededCitizen = context.Citizens.Find(1);
            Assert.NotNull(seededCitizen);
            Assert.Equal("Zara Windcroft", seededCitizen.FullName);
        }
    }

    [Fact]
    public void CanDeleteCitizen_AndCascadeDeleteServiceRequests()
    {
        using (var context = GetDbContext())
        {
            var citizen = new Citizen { CitizenID = 99, FullName = "Delete Me", Address = "N/A", PhoneNumber = "0000000000", Email = "delete@mail.com" };
            context.Citizens.Add(citizen);
            context.ServiceRequests.Add(new ServiceRequest { RequestID = 99, CitizenID = 99, ServiceType = "Test Service Type" });
            context.SaveChanges();
        }

        using (var context = GetDbContext())
        {
            var citizen = context.Citizens.Include(c => c.ServiceRequests).FirstOrDefault(c => c.CitizenID == 99);
            Assert.NotNull(citizen);
            Assert.Single(citizen.ServiceRequests);

            context.Citizens.Remove(citizen);
            context.SaveChanges();
        }

        using (var context = GetDbContext())
        {
            Assert.Null(context.Citizens.Find(99));
            Assert.Null(context.ServiceRequests.Find(99));
        }
    }

    [Fact]
    public void CanUpdateReportStatus()
    {
        using (var context = GetDbContext())
        {
            var report = context.Reports.FirstOrDefault(r => r.ReportID == 1);
            Assert.NotNull(report);
            report.Status = "Approved";
            context.SaveChanges();
        }

        using (var context = GetDbContext())
        {
            var updatedReport = context.Reports.Find(1);
            Assert.Equal("Approved", updatedReport?.Status);
        }
    }
}
