using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMS_ADP631_FA1.Controllers;
using MMS_ADP631_FA1.Data;
using MMS_ADP631_FA1.Models;
using Xunit;

namespace MMS_ADP631_FA1.Tests.Controllers
{
    public class ReportControllerTests
    {
        private ReportsController? _controller;
        private DbContextOptions<ApplicationDbContext> _options;

        public ReportControllerTests()
        {
            // Setting up an in memory db
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_ReportsDB")
                .Options;
        
            // Create the database
            using (var context = new ApplicationDbContext(_options))
            {
                context.Database.EnsureCreated();
            }
        }

        [Fact]
        public void GetReports_ReturnsReportsAsList()
        {
            using (var context = new ApplicationDbContext(_options))
            {
                var staff = new Staff 
                { 
                    FullName = "George Lucas", 
                    Position = "Visionary", 
                    Department = "Main", 
                    Email = "GeorgeL@email.com", 
                    PhoneNumber = "123 333 4455" 
                };
                context.Staff.Add(staff);
                context.SaveChanges();
                _controller = new ReportsController(context);
                context.Reports.Add(new Report { ReportType = "Damage To Property", Details = "Report Details", Status = "Pending" , StaffID = staff.StaffID});
                context.SaveChanges();

                var result = _controller.Index() as ViewResult;
                var model = result?.Model as Tuple<List<Report>, List<Staff>>;
                Assert.NotNull(model);
                Assert.NotNull(model.Item1);
                Assert.NotEmpty(model.Item1);

                var addedReport = model.Item1.FirstOrDefault(r => r.ReportType == "Damage To Property");
                Assert.NotNull(addedReport);
                Assert.Equal("Damage To Property", addedReport.ReportType);
            }
        }

        [Fact]
        public void CreateReport_AddsReport()
        {
            using (var context = new ApplicationDbContext(_options))
            {
                var newReport = new Report 
                { 
                    ReportType = "Refuse Removal", 
                    Details = "Garbage Collection Day Skipped", 
                    Status = "Pending" 
                };
                _controller = new ReportsController(context);
                var result = _controller.Create(newReport) as RedirectToActionResult;
                
                Assert.NotNull(result);
                Assert.Equal("Index", result.ActionName);
            }

            using (var context = new ApplicationDbContext(_options))
            {
                var addedReport = context.Reports.FirstOrDefault(r => r.ReportType == "Refuse Removal");
                Assert.NotNull(addedReport);
                Assert.Equal("Refuse Removal", context.Reports.Last().ReportType);
            }
        }
    }
}