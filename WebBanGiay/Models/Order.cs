using System;
using System.Collections.Generic;

namespace WebBanGiay.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public DateTime? OrderDate { get; set; }

    public string ShippingAddress { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public double TotalAmount { get; set; }

    public int? PaymentMethodId { get; set; }

    public string? ShippingStatus { get; set; }

    public int? UserId { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual PaymentMethod? PaymentMethod { get; set; }

    public virtual User? User { get; set; }
}
