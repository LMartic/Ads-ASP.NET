using System.ComponentModel.DataAnnotations;

namespace Ads.Application.Dto
{
    public class CreateFollowerDto
    {
        public int AdId { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}