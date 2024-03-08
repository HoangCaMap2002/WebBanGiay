using System;
using System.Collections.Generic;

namespace WebBanGiay.Models;

public partial class UserTypeUser
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int TypeUserId { get; set; }

    public virtual TypeUser TypeUser { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
