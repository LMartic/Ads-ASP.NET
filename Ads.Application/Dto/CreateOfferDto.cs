using System.ComponentModel.DataAnnotations;

namespace Ads.Application.Dto
{
    public class CreateOfferDto
    {
        [Required]
        public string UserId { get; set; }

        public int AdId { get; set; }

        public decimal Amount { get; set; }
    }
}