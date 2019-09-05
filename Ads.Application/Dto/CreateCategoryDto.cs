using System.ComponentModel.DataAnnotations;

namespace Ads.Application.Dto
{
    public class CreateCategoryDto
    {
        [Required]
        public string Name { get; set; }

    }
}