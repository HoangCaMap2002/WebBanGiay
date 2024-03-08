using System;
using System.Collections.Generic;

namespace WebBanGiay.Models;

public partial class OrderDetail
{
    public int DetailId { get; set; }

    public int? OrderId { get; set; }

    public int? ProductId { get; set; }

    public int Quantity { get; set; }

    public double PricePerUnit { get; set; }

    public string? SizeName { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Product? Product { get; set; }
}
