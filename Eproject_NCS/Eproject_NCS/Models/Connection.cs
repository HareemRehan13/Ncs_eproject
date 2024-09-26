using System;
using System.Collections.Generic;

namespace Eproject_NCS.Models;

public partial class Connection
{
    public int ConnectionId { get; set; }

    public string? ConnectionNo { get; set; }

    public int CustomerId { get; set; }

    public string ConnectionType { get; set; } = null!;

    public int PlanId { get; set; }

    public DateTime StartDate { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<Billing> Billings { get; set; } = new List<Billing>();

    public virtual Customer Customer { get; set; } = null!;

    public virtual ServicePlan Plan { get; set; } = null!;
}
