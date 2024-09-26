using System;
using System.Collections.Generic;

namespace Eproject_NCS.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public string ProductType { get; set; } = null!;

    public int Stock { get; set; }

    public decimal Price { get; set; }

    public string Image { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
