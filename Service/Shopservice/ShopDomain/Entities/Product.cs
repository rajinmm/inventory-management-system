using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopDomain.Common;

namespace ShopDomain.Entities
{
    public class Product : BaseEntity
    {

        public string Name { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public float BaseDiscountInPercentage { get; set; }

        // Navigation properties
        public virtual Category Category { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
