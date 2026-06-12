using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopDomain.Common;

namespace ShopDomain.Entities
{
    public class Category : BaseEntity
    {
        public string CategoryName { get; set; }
        // Navigation property
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
