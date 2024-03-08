using System;
using System.Collections.Generic;

namespace WebBanGiay.Models;

public partial class ProductSize
{
    public int ProductId { get; set; }

    public int SizeId { get; set; }

    public int? Quanity { get; set; }

    public int Id { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Size Size { get; set; } = null!;
}
