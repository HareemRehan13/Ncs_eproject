using System;
using System.Collections.Generic;

namespace Eproject_NCS.Models;

public partial class RetailShop
{
    public int ShopId { get; set; }

    public string ShopName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string ContactNumber { get; set; } = null!;
}
