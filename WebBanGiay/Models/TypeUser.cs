using System;
using System.Collections.Generic;

namespace WebBanGiay.Models;

public partial class TypeUser
{
    public int TypeUserId { get; set; }

    public string? NameTyPe { get; set; }

    public virtual ICollection<UserTypeUser> UserTypeUsers { get; set; } = new List<UserTypeUser>();
}
