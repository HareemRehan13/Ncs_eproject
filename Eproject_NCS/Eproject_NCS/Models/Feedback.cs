using System;
using System.Collections.Generic;

namespace Eproject_NCS.Models;

public partial class Feedback
{
    public int FeedbackId { get; set; }

    public int? CustomerId { get; set; }

    public string? OrderId { get; set; }

    public string? FeedbackText { get; set; }

    public DateTime FeedbackDate { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Order? Order { get; set; }
}
