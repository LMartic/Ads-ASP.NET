using System.ComponentModel.DataAnnotations;

namespace Ads.Application.Dto
{
    public class DeleteOfferDto
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}