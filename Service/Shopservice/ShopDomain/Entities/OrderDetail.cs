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
    public class OrderDetail : BaseEntity
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Qty { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal NetAmount { get; set; }

        // Navigation properties
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
