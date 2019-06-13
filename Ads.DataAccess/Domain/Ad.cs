using System;

namespace Ads.DataAccess.Domain
{
    public class Ad
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int SubCategoryId { get; set; }

        public DateTime AddedDateTime { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual SubCategory SubCategory { get; set; }
    }
}