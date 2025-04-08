using System;
using System.Collections.Generic;

namespace Quiq_Application.Entity;

public partial class UserAddress
{
    public int UserAddressId { get; set; }

    public string UserAddressName { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
