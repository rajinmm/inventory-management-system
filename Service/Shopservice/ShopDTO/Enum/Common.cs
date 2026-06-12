using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDTO.Enum
{
    public enum ProductCategory
    {
        Electronics,
        Clothing,
        HomeGoods,
        Books,
        Toys,
        Beauty,
        Sports,
        Automotive
    }
    public enum OrderStatus
    {
        Pending,
        Processing,
        Shipped,
        Delivered,
        Cancelled
    }

}
