using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using MMS_ADP631_FA1.Controllers;
using MMS_ADP631_FA1.Data;
using MMS_ADP631_FA1.Models;
using Xunit;

public class StaffControllerTests
{
    private StaffController? _controller;
    private DbContextOptions<ApplicationDbContext> _options;

    public StaffControllerTests()
    {
        // Setting up an in memory db
        _options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "Test_StaffDB")
            .Options;

        // Create the database
        using (var context = new ApplicationDbContext(_options))
        {
            context.Database.EnsureCreated();
        }
    }

    [Fact]
    public void GetStaff_ReturnsStaffAsList()
    {
        using (var context = new ApplicationDbContext(_options))
        {
            context.Staff.Add(new Staff { FullName = "Philip Dumphey", Position = "Parks Manager", Department = "Parks", Email = "PhilD@email.com", PhoneNumber = "123 333 4455" });
            context.SaveChanges();
            _controller = new StaffController(context);

            var result = _controller.Index() as ViewResult;
            var model = result?.Model as List<Staff>;

            Assert.NotNull(model);
            var addedStaff = model?.FirstOrDefault(s => s.FullName == "Philip Dumphey");
            Assert.NotNull(addedStaff);
            Assert.Equal("Philip Dumphey", addedStaff.FullName);
        }
    }

    [Fact]
    public void CreateStaff_AddStaff()
    {
        using (var context = new ApplicationDbContext(_options))
        {
            var newStaff = new Staff { FullName = "Caleb Montgumry", Position = "Electrician", Department = "Maintenance", Email = "CalebM@email.com", PhoneNumber = "123 333 4455" };
            _controller = new StaffController(context);
            var result = _controller.Create(newStaff) as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }

        using (var context = new ApplicationDbContext(_options))
        {
            var addedStaff = context.Staff.FirstOrDefault(s => s.FullName == "Philip Dumphey");
            Assert.NotNull(addedStaff);
            Assert.Equal("Caleb Montgumry", context.Staff.Last().FullName);
        }
    }
}