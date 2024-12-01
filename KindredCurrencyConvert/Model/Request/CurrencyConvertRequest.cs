using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KindredCurrencyConvert.Model.Request
{
    public class CurrencyConvertRequest
    {
        public decimal Amount { get; set; }
        public string InputCurrency { get; set; }
        public string OutputCurrency { get; set; }
    }
}
