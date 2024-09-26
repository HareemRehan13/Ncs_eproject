using System;
using System.Collections.Generic;

namespace Eproject_NCS.Models;

public partial class Billing
{
    public int BillingId { get; set; }

    public int? CustomerId { get; set; }

    public int? ConnectionId { get; set; }

    public DateTime BillDate { get; set; }

    public decimal TotalAmount { get; set; }

    public virtual Connection? Connection { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
