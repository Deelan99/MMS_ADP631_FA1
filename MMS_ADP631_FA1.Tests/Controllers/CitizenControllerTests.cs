using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMS_ADP631_FA1.Data;
using MMS_ADP631_FA1.Models;
using MyApp.Namespace;
using Xunit;

namespace MMS_ADP631_FA1.Tests.Controllers
{
    public class CitizenControllerTests
    {
        private CitizenController? _controller;
        private DbContextOptions<ApplicationDbContext> _options;

        public CitizenControllerTests()
        {
            // Setting up an in memory db
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("Test_CitizenDB")
                .Options;

            // Create the database
            using (var context = new ApplicationDbContext(_options))
            {
                context.Database.EnsureCreated();
            }
        }

        [Fact]
        public void GetCitizens_ReturnsLIstOfCitizens()
        {
            using (var context = new ApplicationDbContext(_options))
            {
                context.Citizens.Add(new Citizen { FullName = "John Doe", Address = "123 Test Street", PhoneNumber = "123 333 4455", Email = "johnd@testemail.com" });
                context.SaveChanges();
                _controller = new CitizenController(context);

                var result = _controller.Index() as ViewResult;
                var model = result?.Model as List<Citizen>;

                Assert.NotNull(model);
                var addedCitizen = model?.FirstOrDefault(c => c.FullName == "John Doe");
                Assert.NotNull(addedCitizen);
                Assert.Equal("John Doe", addedCitizen.FullName);
            }
        }

        [Fact]
        public void CreateCitizen_AddsCitizen()
        {
            using (var context = new ApplicationDbContext(_options))
            {
                var newCitizen = new Citizen
                {
                    FullName = "Jane Doe",
                    Address = "123 Test Street",
                    PhoneNumber = "123 333 4455",
                    Email = "jane@example.com"
                };
                _controller = new CitizenController(context);
                var result = _controller.Create(newCitizen) as RedirectToActionResult;

                Assert.NotNull(result);
                Assert.Equal("Index", result.ActionName);

            }

            // Check if new request was added to the database
            using (var context = new ApplicationDbContext(_options))
            {
                var addedCitizen = context.Citizens.FirstOrDefault(c => c.FullName == "Jane Doe");
                
                Assert.NotNull(addedCitizen);
                Assert.Equal("Jane Doe", context.Citizens.Last().FullName);
            }
        }
    }
}