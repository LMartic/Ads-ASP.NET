using System.Collections.Generic;

namespace Ads.DataAccess.Domain
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Ad> Ads { get; set; }

        public Category()
        {
            Ads = new HashSet<Ad>();
        }
    }
}