using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SubSentinel.API.Data;
using SubSentinel.API.Models;
using SubSentinel.API.DTOs;

namespace SubSentinel.API.Controllers
{
    [Route("api/[controller]")] 
    [ApiController]
    public class SubscriptionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SubscriptionsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subscription>>> GetSubscriptions()
        {
            return await _context.Subscriptions.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Subscription>> CreateSubscription(DTOs.SubscriptionCreateDto subDto)
        {
            var subscription = new Subscription
            {
                ServiceName = subDto.ServiceName,
                MonthlyCost = subDto.MonthlyCost,
                NextRenewalDate = subDto.NextRenewalDate,
                SubscriberEmail = subDto.SubscriberEmail,
                IsActive = true 
            };

            _context.Subscriptions.Add(subscription);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSubscriptions), new { id = subscription.Id }, subscription);
        }
    }
}