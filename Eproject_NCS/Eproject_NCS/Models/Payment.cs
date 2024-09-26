using System;
using System.Collections.Generic;

namespace Eproject_NCS.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int? CustomerId { get; set; }

    public int? BillingId { get; set; }

    public DateTime PaymentDate { get; set; }

    public decimal Amount { get; set; }

    public virtual Billing? Billing { get; set; }

    public virtual Customer? Customer { get; set; }
}
