using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopDomain.Common;
using ShopInfrastructure;

namespace ShopDomain.Entities
{
    public class UserRole : BaseEntity
    {
         
        public string Role { get; set; }

        // Navigation property
        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
