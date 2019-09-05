namespace Ads.Application.Searches
{
    public class AdSearch
    {
        public int Id { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }

        public string CategoryName { get; set; }

        public string SubCategoryName { get; set; }

        public string Email { get; set; }
        public string FollowerUserId { get; set; }
    }
}