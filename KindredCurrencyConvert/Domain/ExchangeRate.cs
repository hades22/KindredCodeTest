using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KindredCurrencyConvert.Domain
{
    public class ExchangeRate
    {
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
        public decimal Rate { get; set; }
        public DateTime ValidUntil { get; set; }
        public Status Status { get; set; }
    }
}
