using System;
using System.Collections.Generic;

namespace Eproject_NCS.Models;

public partial class Order
{
    public string OrderId { get; set; } = null!;

    public int CustomerId { get; set; }

    public DateTime OrderDate { get; set; }

    public string Status { get; set; } = null!;

    public int EquipmentId { get; set; }

    public virtual User Customer { get; set; } = null!;

    public virtual Product Equipment { get; set; } = null!;

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
}
