using System;
using System.Collections.Generic;

namespace Eproject_NCS.Models;

public partial class ConnectionOrder
{
    public string ConordId { get; set; } = null!;

    public int PlanId { get; set; }

    public int CustomerId { get; set; }

    public int? Total { get; set; }

    public string Status { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;

    public virtual ServicePlan Plan { get; set; } = null!;
}
