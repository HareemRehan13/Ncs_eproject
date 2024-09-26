using System;
using System.Collections.Generic;

namespace Eproject_NCS.Models;

public partial class Vendor
{
    public int VendorId { get; set; }

    public string VendorName { get; set; } = null!;

    public string? ContactPerson { get; set; }

    public string ContactNumber { get; set; } = null!;

    public string? Email { get; set; }
}
