using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMS_ADP631_FA1.Controllers;
using MMS_ADP631_FA1.Data;
using MMS_ADP631_FA1.Models;
using Xunit;

public class ReportControllerTests
{
    private DbContextOptions<ApplicationDbContext> _options;

    public ReportControllerTests()
    {
        _options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "Test_ReportsDB")
            .Options;
    }

    [Fact]
    public void GetReports_ReturnsReportsAsList()
    {
        using (var context = new ApplicationDbContext(_options))
        {
            context.Reports.Add(new Report { ReportID = 1, ReportType = "Damage To Property", Details = "Report Details", Status = "Pending" });
            context.SaveChanges();
        }

        using (var context = new ApplicationDbContext(_options))
        {
            var controller = new ReportsController(context);
            var result = controller.Index() as ViewResult;
            var model = result?.Model as Tuple<List<Report>, List<Staff>>;

            Assert.NotNull(model);
            Assert.Single(model.Item1);
            Assert.Equal("Damage To Property", model.Item1[0].ReportType);
        }
    }

    [Fact]
    public void CreateReport_AddsReport()
    {
        using (var context = new ApplicationDbContext(_options))
        {
            var controller = new ReportsController(context);
            var newReport = new Report { ReportType = "Refuse Removal", Details = "Garbage Collection Day Skipped", Status = "Pending" };

            controller.ModelState.Clear(); // Ensure ModelState is valid
            var result = controller.Create(newReport) as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }

        using (var context = new ApplicationDbContext(_options))
        {
            Assert.Equal(2, context.Reports.Count());
            Assert.Equal("Refuse Removal", context.Reports.Last().ReportType);
        }
    }
}