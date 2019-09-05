using System;
using System.Collections.Generic;

namespace Ads.DataAccess.Domain
{
    public class Ad
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int CategoryId { get; set; }

        public DateTime AddedDateTime { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual Category Category { get; set; }

        public ICollection<Follower> Followers { get; set; }
        public ICollection<Offer> Offers { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public Ad()
        {
            Followers = new HashSet<Follower>();
            Offers = new HashSet<Offer>();
            Comments = new HashSet<Comment>();
        }
    }
}