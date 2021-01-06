using System;
using System.Collections.Generic;
using System.Text;

namespace SI_Master.Common
{
    public enum QueryMode
    {
        Contracts = 0,
        Payments,
        Cards,
        Transactions,
        RequestLimits,
        RequestCards,
        RequestBlocking,
        Settlements
    }
}
