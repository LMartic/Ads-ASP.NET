using System.Collections.Generic;

namespace Ads.DataAccess.Domain
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<SubCategory> SubCategories { get; set; }

        public Category()
        {
            SubCategories = new HashSet<SubCategory>();
        }


    }
}