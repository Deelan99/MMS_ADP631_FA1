using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMS_ADP631_FA1.Controllers;
using MMS_ADP631_FA1.Data;
using MMS_ADP631_FA1.Models;
using Xunit;

public class ServiceRequestControllerTests
{
    private ServiceRequestController _controller;
    private DbContextOptions<ApplicationDbContext> _options;

    public ServiceRequestControllerTests()
    {
        _options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("Test_ServiceRequestDB")
            .Options;

        using (var context = new ApplicationDbContext(_options))
        {
            context.ServiceRequests.Add(new ServiceRequest { RequestID = 1, ServiceType = "Grass Cutting", Status = "Pending" });
            context.SaveChanges();
        }

        var dbContext = new ApplicationDbContext(_options);
        _controller = new ServiceRequestController(dbContext);
    }

    [Fact]
    public void GetServiceRequests_ReturnsServiceRequestAsList()
    {
        var result = _controller.Index() as ViewResult;
        var model = result?.Model as List<ServiceRequest>;

        Assert.NotNull(model);
        Assert.Single(model);
        Assert.Equal("Grass Cutting", model[0].ServiceType);
    }

    [Fact]
    public void CreateServiceRequest_AddRequest()
    {
        var newRequest = new ServiceRequest { ServiceType = "Pothole on Main Road", Status = "Pending" };

        var result = _controller.Create(newRequest) as RedirectToActionResult;

        Assert.NotNull(result);
        Assert.Equal("Index", result.ActionName);

        using (var context = new ApplicationDbContext(_options))
        {
            Assert.Equal(2, context.ServiceRequests.Count());
            Assert.Equal("Pothole on Main Road", context.ServiceRequests.Last().ServiceType);
        }
    }

}