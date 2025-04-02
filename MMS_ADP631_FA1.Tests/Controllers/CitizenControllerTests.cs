using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMS_ADP631_FA1.Data;
using MMS_ADP631_FA1.Models;
using MyApp.Namespace;
using Xunit;

public class CitizenControllerTests
{
    private DbContextOptions<ApplicationDbContext> _options;

    public CitizenControllerTests()
    {
        // Setting up an in memory db
        _options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "Test_CitizenDB")
            .Options;
    }

    [Fact]
    public void GetCitizens_ReturnsLIstOfCitizens()
    {
        using (var context = new ApplicationDbContext(_options))
        {
            context.Citizens.Add(new Citizen { CitizenID = 1, FullName = "John Doe", Address = "123 Test Street", PhoneNumber = "123 333 4455", Email = "johnd@testemail.com" });
            context.SaveChanges();
        }


        using (var context = new ApplicationDbContext(_options))
        {
            var controller = new CitizenController(context);
            var result = controller.Index() as ViewResult;
            var model = result?.Model as List<Citizen>;

            Assert.NotNull(model);
            Assert.Single(model);
            Assert.Equal("John Doe", model[0].FullName);
        }
    }

    [Fact]
    public void CreateCitizen_AddsCitizen()
    {
        using (var context = new ApplicationDbContext(_options))
        {
            var controller = new CitizenController(context);
            var newCitizen = new Citizen
            {
                FullName = "Jane Doe",
                Address = "123 Test Street",
                PhoneNumber = "123 333 4455",
                Email = "jane@example.com"
            };

            controller.ModelState.Clear(); // Ensuring ModelState is valid
            var result = controller.Create(newCitizen) as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }

        using (var context = new ApplicationDbContext(_options))
        {
            Assert.Equal(2, context.Citizens.Count());
            Assert.Equal("Jane Doe", context.Citizens.Last().FullName);
        }

    }
}