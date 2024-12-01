using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KindredCurrencyConvert.Model.Response
{
    public class CurrencyConvertResponse
    {
        public decimal Amount { get; set; }
        public string InputCurrency { get; set; }
        public string OutputCurrency { get; set; }
        public decimal Value { get; set; }
    }
}
