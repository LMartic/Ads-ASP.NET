using System.ComponentModel.DataAnnotations;

namespace Ads.Application.Dto
{
    public class CreateCategoryDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

    }
}