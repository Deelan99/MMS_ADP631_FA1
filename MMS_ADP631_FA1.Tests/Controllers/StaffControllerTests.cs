using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMS_ADP631_FA1.Controllers;
using MMS_ADP631_FA1.Data;
using MMS_ADP631_FA1.Models;
using Xunit;

public class StaffControllerTests
{
    private StaffController _controller;
    private DbContextOptions<ApplicationDbContext> _options;

    public StaffControllerTests()
    {
        // Setting up an in memory db
        _options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "Test_StaffDB")
            .Options;
        using (var context = new ApplicationDbContext(_options))
        {
            context.Staff.Add(new Staff { StaffID = 1, FullName = "Philip Dumphey", Position = "Parks Manager", Department = "Parks", Email = "PhilD@email.com", PhoneNumber = "123 333 4455" });
            context.SaveChanges();
        }

        var dbContext = new ApplicationDbContext(_options);
        _controller = new StaffController(dbContext);
    }

    [Fact]
    public void GetStaff_ReturnsStaffAsList()
    {
        var result = _controller.Index() as ViewResult;
        var model = result?.Model as List<Staff>;

        Assert.NotNull(model);
        Assert.Single(model);
        Assert.Equal("Philip Dumphey", model[0].FullName);
    }


    [Fact]
    public void CreateStaff_AddStaff()
    {
        var newStaff = new Staff { FullName = "Caleb Montgumry", Position = "Electrician", Department = "Maintenance" };

        var result = _controller.Create(newStaff) as RedirectToActionResult;
    
        Assert.NotNull(result);
        Assert.Equal("Index", result.ActionName);

        using (var context = new ApplicationDbContext(_options))
        {
            Assert.Equal(2, context.Staff.Count());
            Assert.Equal("Caleb Montgumry", context.Staff.Last().FullName);
        }
    }
}