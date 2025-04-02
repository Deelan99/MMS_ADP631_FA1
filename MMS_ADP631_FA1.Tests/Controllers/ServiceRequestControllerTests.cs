using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMS_ADP631_FA1.Controllers;
using MMS_ADP631_FA1.Data;
using MMS_ADP631_FA1.Models;
using Xunit;

namespace MMS_ADP631_FA1.Tests.Controllers
{
    public class ServiceRequestControllerTests
    {
        private ServiceRequestController? _controller;
        private DbContextOptions<ApplicationDbContext> _options;

        public ServiceRequestControllerTests()
        {
            // Setting up an in memory db
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("Test_ServiceRequestDB")
                .Options;

            // Create the database
            using (var context = new ApplicationDbContext(_options))
            {
                context.Database.EnsureCreated();
            }
        }

        [Fact]
        public void GetServiceRequests_ReturnsServiceRequestAsList()
        {
            using (var context = new ApplicationDbContext(_options))
            {
                var citizen = new Citizen { FullName = "Dude Perfect", Address = "123 Dude Street", PhoneNumber = "1234567890", Email = "DudeP@example.com" };
                context.Citizens.Add(citizen);
                context.SaveChanges();

                _controller = new ServiceRequestController(context);
                context.ServiceRequests.Add(new ServiceRequest { ServiceType = "Grass Cutting", Status = "Pending" });
                context.SaveChanges();

                var result = _controller.Index() as ViewResult;
                var model = result?.Model as Tuple<List<ServiceRequest>, List<Citizen>>;
                Assert.NotNull(model);
                Assert.NotNull(model.Item1);
                Assert.NotEmpty(model.Item2);

                var addedServiceRequest = model.Item1.FirstOrDefault(sr => sr.ServiceType == "Grass Cutting");
                Assert.NotNull(addedServiceRequest);
                Assert.Equal("Grass Cutting", addedServiceRequest.ServiceType);
            }
        }

        [Fact]
        public void CreateServiceRequest_AddRequest()
        {
            using (var context = new ApplicationDbContext(_options))
            {
                var citizen = new Citizen { FullName = "John Doe", Address = "123 Test Street", PhoneNumber = "1234567890", Email = "johnd@example.com" };
                context.Citizens.Add(citizen);
                context.SaveChanges();

                _controller = new ServiceRequestController(context);
                var newRequest = new ServiceRequest { ServiceType = "Pothole on Main Road", Status = "Pending", CitizenID = citizen.CitizenID };
                _controller.ModelState.Clear();
                var result = _controller.Create(newRequest) as RedirectToActionResult;

                Assert.NotNull(result);
                Assert.Equal("Index", result.ActionName);
            }

            // Check if new request was added to the database
            using (var context = new ApplicationDbContext(_options))
            {
                var addedRequest = context.ServiceRequests
                                    .FirstOrDefault(r => r.ServiceType == "Pothole on Main Road");

                Assert.NotNull(addedRequest); // Ensure the request was added
                Assert.Equal("Pothole on Main Road", addedRequest.ServiceType);
            };
        }
    }

}