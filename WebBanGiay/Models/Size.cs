using System;
using System.Collections.Generic;

namespace WebBanGiay.Models;

public partial class Size
{
    public int SizeId { get; set; }

    public string SizeValue { get; set; } = null!;

    public virtual ICollection<ProductSize> ProductSizes { get; set; } = new List<ProductSize>();
}
