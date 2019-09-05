using System.ComponentModel.DataAnnotations;

namespace Ads.Application.Dto
{
    public class EditOfferDto
    {
        public int Id { get; set; }

        public decimal Amount { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}