using System;
using System.Collections.Generic;

namespace CareBridge.EFCoreDemo.Models.Generated;

public partial class BillingClaim
{
    public int ClaimId { get; set; }

    public int PatientId { get; set; }

    public string Status { get; set; } = null!;

    public decimal BilledAmount { get; set; }

    public decimal? ReimbursedAmt { get; set; }
}
