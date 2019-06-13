using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Ads.DataAccess.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Ad> Ads { get; set; }
        public ApplicationUser()
        {
            Ads = new HashSet<Ad>();
        }
    }
}