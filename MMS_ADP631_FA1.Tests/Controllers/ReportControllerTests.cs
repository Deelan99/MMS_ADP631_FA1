using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMS_ADP631_FA1.Controllers;
using MMS_ADP631_FA1.Data;
using MMS_ADP631_FA1.Models;
using Xunit;

public class ReportControllerTests
{
    private ReportsController _controller;
    private DbContextOptions<ApplicationDbContext> _options;

    public ReportControllerTests()
    {
        // Setting up an in memory db
        _options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "Test_ReportsDB")
            .Options;

        using (var context = new ApplicationDbContext(_options))
        {
            context.Reports.Add(new Report { ReportID = 1, ReportType = "Damage To Property", Details = "Report Details", Status = "Pending" });
            context.SaveChanges();
        }

        var dbContext = new ApplicationDbContext(_options);
        _controller = new ReportsController(dbContext);
    }

    [Fact]
    public void GetReports_ReturnsReportsAsList()
    {
        var result = _controller.Index() as ViewResult;
        var model = result?.Model as List<Report>;

        Assert.NotNull(model);
        Assert.Single(model);
        Assert.Equal("Damage To Property", model[0].ReportType);
    }

    [Fact]
    public void CreateReport_AddsReport()
    {
        var newReport = new Report { ReportID = 2, ReportType = "Refuse Removal", Details = "Garbage Collection Day Skipped", Status = "Pending" };

        var result = _controller.Create(newReport) as RedirectToActionResult;

        Assert.NotNull(result);
        Assert.Equal("Index", result.ActionName);

        using (var context = new ApplicationDbContext(_options))
        {
            Assert.Equal(2, context.Reports.Count());
            Assert.Equal("Refuse Removal", context.Reports.Last().ReportType);
        }
    }
}