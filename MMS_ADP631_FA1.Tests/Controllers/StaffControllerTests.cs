using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMS_ADP631_FA1.Controllers;
using MMS_ADP631_FA1.Data;
using MMS_ADP631_FA1.Models;
using Xunit;

public class StaffControllerTests
{
    private DbContextOptions<ApplicationDbContext> _options;

    public StaffControllerTests()
    {
        _options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "Test_StaffDB")
            .Options;
    }

    [Fact]
    public void GetStaff_ReturnsStaffAsList()
    {
        using (var context = new ApplicationDbContext(_options))
        {
            context.Staff.Add(new Staff { StaffID = 1, FullName = "Philip Dumphey", Position = "Parks Manager", Department = "Parks", Email = "PhilD@email.com", PhoneNumber = "123 333 4455" });
            context.SaveChanges();
        }

        using (var context = new ApplicationDbContext(_options))
        {
            var controller = new StaffController(context);
            var result = controller.Index() as ViewResult;
            var model = result?.Model as List<Staff>;

            Assert.NotNull(model);
            Assert.Single(model);
            Assert.Equal("Philip Dumphey", model[0].FullName);
        }
    }


    [Fact]
    public void CreateStaff_AddsStaff()
    {
        using (var context = new ApplicationDbContext(_options))
        {
            var controller = new StaffController(context);
            var newStaff = new Staff { FullName = "Caleb Montgumry", Position = "Electrician", Department = "Maintenance", Email = "CalebM@email.com", PhoneNumber = "123 333 4455" };

            controller.ModelState.Clear(); // Ensure ModelState is valid
            var result = controller.Create(newStaff) as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }

        using (var context = new ApplicationDbContext(_options))
        {
            Assert.Equal(2, context.Staff.Count());
            Assert.Equal("Caleb Montgumry", context.Staff.Last().FullName);
        }
    }
}