namespace Ads.Application.Dto
{
    public class CreateAdDto
    {
        public int Id { get; set; }

        public int SubCategoryId { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }

        public string UserId { get; set; }
    }
}