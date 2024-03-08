using System;
using System.Collections.Generic;

namespace WebBanGiay.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public int? BrandId { get; set; }

    public double Price { get; set; }

    public string? Description { get; set; }

    public int Quantity { get; set; }

    public string Img { get; set; } = null!;

    public virtual Brand? Brand { get; set; }

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

    public virtual ICollection<ProductSize> ProductSizes { get; set; } = new List<ProductSize>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<SalesHistory> SalesHistories { get; set; } = new List<SalesHistory>();
}
