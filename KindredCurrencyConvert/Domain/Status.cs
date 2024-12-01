using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KindredCurrencyConvert.Domain
{
    public enum Status : int
    {
        Ok = 0,
        ExchangeRateNotFound = 1,
    }
}
