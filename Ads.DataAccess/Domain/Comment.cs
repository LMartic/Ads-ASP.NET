namespace Ads.DataAccess.Domain
{
    public class Comment
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int AdId { get; set; }

        public string Comments { get; set; }

        public Ad Ad { get; set; }

        public ApplicationUser User { get; set; }
    }
}