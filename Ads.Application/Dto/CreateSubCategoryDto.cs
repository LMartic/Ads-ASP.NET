using System.ComponentModel.DataAnnotations;

namespace Ads.Application.Dto
{
    public class CreateSubCategoryDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
    }
}