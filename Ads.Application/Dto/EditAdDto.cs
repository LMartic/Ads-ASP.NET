using System.ComponentModel.DataAnnotations;

namespace Ads.Application.Dto
{
    public class EditAdDto
    {
        [Required]
        public int Id { get; set; }
        public string UserId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public string Subject { get; set; }
        [Required]

        public string Description { get; set; }
    }
}