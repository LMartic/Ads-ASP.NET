namespace Ads.DataAccess.Domain
{
    public class Offer
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public decimal Amount { get; set; }

        public int AdId { get; set; }

        public virtual Ad Ad { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}