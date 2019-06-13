using System.Collections.Generic;

namespace Ads.DataAccess.Domain
{
    public class SubCategory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public ICollection<Ad> Ads { get; set; }

        public SubCategory()
        {
            Ads = new HashSet<Ad>();
        }
    }
}