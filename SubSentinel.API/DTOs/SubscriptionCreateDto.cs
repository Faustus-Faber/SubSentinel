using System.ComponentModel.DataAnnotations;

namespace SubSentinel.API.DTOs
{
    public class SubscriptionCreateDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public required string ServiceName { get; set; }

        [Range(0.01, 10000)]
        public decimal MonthlyCost { get; set; }

        [Required]
        public DateTime NextRenewalDate { get; set; }

        [Required]
        [EmailAddress] 
        public required string SubscriberEmail { get; set; }
    }
}