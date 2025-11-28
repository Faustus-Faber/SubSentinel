using Microsoft.EntityFrameworkCore;
using SubSentinel.API.Data;

namespace SubSentinel.API.Services
{
    public class SubscriptionRenewalService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<SubscriptionRenewalService> _logger;

        public SubscriptionRenewalService(IServiceScopeFactory scopeFactory, ILogger<SubscriptionRenewalService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("--- Sentinel is scanning for expiring subscriptions... ---");

                // Create a scope explicitly to get the database
                using (var scope = _scopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                    // Logic: Find subscriptions expiring in the next 7 days
                    var today = DateTime.Today;
                    var warningWindow = today.AddDays(7);

                    var expiringSubs = await context.Subscriptions
                        .Where(s => s.NextRenewalDate.Date >= today && s.NextRenewalDate.Date <= warningWindow)
                        .ToListAsync(stoppingToken);

                    foreach (var sub in expiringSubs)
                    {
                        // SIMULATION: In a real app, we would send an email here.
                        // For now, we log a High Priority Warning to the console.
                        _logger.LogWarning($"[ALERT] RENEWAL IMMINENT: {sub.ServiceName} renews on {sub.NextRenewalDate.ToShortDateString()} for ${sub.MonthlyCost}!");
                    }
                }

                // Wait for 10 seconds before checking again (For testing purposes)
                // In production, use: await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
        }
    }
}