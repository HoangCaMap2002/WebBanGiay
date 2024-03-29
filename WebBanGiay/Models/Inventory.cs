﻿using System;
using System.Collections.Generic;

namespace WebBanGiay.Models;

public partial class Inventory
{
    public int InventoryId { get; set; }

    public int? ProductId { get; set; }

    public int? QuantityOnHand { get; set; }

    public DateTime? LastUpdated { get; set; }

    public virtual Product? Product { get; set; }
}
