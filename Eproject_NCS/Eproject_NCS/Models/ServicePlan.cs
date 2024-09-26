using System;
using System.Collections.Generic;

namespace Eproject_NCS.Models;

public partial class ServicePlan
{
    public int PlanId { get; set; }

    public string PlanType { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public decimal? SecurityDeposit { get; set; }

    public virtual ICollection<ConnectionOrder> ConnectionOrders { get; set; } = new List<ConnectionOrder>();

    public virtual ICollection<Connection> Connections { get; set; } = new List<Connection>();
}
