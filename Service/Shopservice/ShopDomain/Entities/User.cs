using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ShopDomain.Common;
using ShopDomain.Entities;

namespace ShopInfrastructure;

public class User : BaseEntity
{
   
    public string Name { get; set; }
    public string UserLogin { get; set; }
    public string Password { get; set; }
    public int RoleId { get; set; }

    // Navigation property
    public virtual UserRole UserRole { get; set; }
}
