using System;
using System.Collections.Generic;

namespace WebBanGiay.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? ImageUser { get; set; }

    public string Password { get; set; } = null!;

    public DateTime? CreateDate { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<SalesHistory> SalesHistories { get; set; } = new List<SalesHistory>();

    public virtual ICollection<UserTypeUser> UserTypeUsers { get; set; } = new List<UserTypeUser>();
}
