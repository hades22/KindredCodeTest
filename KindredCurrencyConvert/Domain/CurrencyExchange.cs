using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KindredCurrencyConvert.Domain
{
    public class CurrencyExchange
    {
        public decimal Amount { get; set; }
        public string InputCurrency { get; set; }
        public string OutputCurrency { get; set; }
        public decimal? ConvertedAmount { get; set; }
        public Status Status { get; set; }
    }
}
