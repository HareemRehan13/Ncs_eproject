using System;
using System.Collections.Generic;

namespace Eproject_NCS.Models;

public partial class DiscountScheme
{
    public int SchemeId { get; set; }

    public int MinConnections { get; set; }

    public int? MaxConnections { get; set; }

    public decimal DiscountPercentage { get; set; }
}
