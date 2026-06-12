using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopDomain.Common;

namespace ShopDomain.Entities
{
    public class Order : BaseEntity
    {       
        public string CustName { get; set; }
        public string CustPhone { get; set; }
        public string OrderNote { get; set; }
        public int OrderStatus { get; set; } // "Pending", "Processing", "Completed", "Cancelled"
        public decimal GrossAmount { get; set; }
        public decimal NetAmount { get; set; }

        // Navigation property
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
