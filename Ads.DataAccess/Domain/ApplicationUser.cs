using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Ads.DataAccess.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Ad> Ads { get; set; }
        public ICollection<Follower> Followers { get; set; }
        public ICollection<Offer> Offers { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ApplicationUser()
        {
            Ads = new HashSet<Ad>();
            Followers = new HashSet<Follower>();
            Comments = new HashSet<Comment>();
        }
    }
}