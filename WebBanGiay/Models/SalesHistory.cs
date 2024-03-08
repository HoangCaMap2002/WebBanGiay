using System;
using System.Collections.Generic;

namespace WebBanGiay.Models;

public partial class SalesHistory
{
    public int TransactionId { get; set; }

    public int? ProductId { get; set; }

    public DateTime? SaleDate { get; set; }

    public int? QuantitySold { get; set; }

    public double? Amount { get; set; }

    public int? UserId { get; set; }

    public virtual Product? Product { get; set; }

    public virtual User? User { get; set; }
}
