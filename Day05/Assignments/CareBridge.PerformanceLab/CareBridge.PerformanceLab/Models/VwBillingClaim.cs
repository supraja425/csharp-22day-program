using System;
using System.Collections.Generic;

namespace CareBridge.PerformanceLab.Models;

public partial class VwBillingClaim
{
    public int ClaimId { get; set; }

    public int PatientId { get; set; }

    public string ClaimStatus { get; set; } = null!;

    public decimal BilledAmount { get; set; }

    public decimal? ReimbursedAmount { get; set; }
}
