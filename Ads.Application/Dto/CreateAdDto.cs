using System.ComponentModel.DataAnnotations;

namespace Ads.Application.Dto
{
    public class CreateAdDto
    {
        public string UserId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public string Subject { get; set; }
        [Required]

        public string Description { get; set; }

        public int Id { get; set; }
    }
}