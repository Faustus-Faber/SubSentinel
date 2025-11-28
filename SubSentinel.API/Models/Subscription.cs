namespace SubSentinel.API.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        public required string ServiceName { get; set; } // e.g., "Netflix", "Azure Trial"
        public decimal MonthlyCost { get; set; }
        public DateTime NextRenewalDate { get; set; }
        public bool IsActive { get; set; } = true;
        public string SubscriberEmail { get; set; } = string.Empty;
    }
}