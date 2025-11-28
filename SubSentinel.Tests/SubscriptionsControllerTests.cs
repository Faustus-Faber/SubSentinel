using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SubSentinel.API.Controllers;
using SubSentinel.API.Data;
using SubSentinel.API.DTOs;
using SubSentinel.API.Models;
using Xunit;

namespace SubSentinel.Tests
{
    public class SubscriptionsControllerTests
    {
        private AppDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
                .Options;
            return new AppDbContext(options);
        }

        [Fact]
        public async Task CreateSubscription_ReturnsCreated_WhenDataIsValid()
        {
            var context = GetInMemoryDbContext();
            var controller = new SubscriptionsController(context);

            var newSub = new SubscriptionCreateDto
            {
                ServiceName = "Test Netflix",
                MonthlyCost = 10.99m,
                NextRenewalDate = DateTime.Today.AddDays(30),
                SubscriberEmail = "test@example.com"
            };

            var result = await controller.CreateSubscription(newSub);

            
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            
            var savedSub = await context.Subscriptions.FirstAsync();
            Assert.Equal("Test Netflix", savedSub.ServiceName);
        }
    }
}