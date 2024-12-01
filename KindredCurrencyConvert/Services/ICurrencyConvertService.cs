using KindredCurrencyConvert.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KindredCurrencyConvert.Services
{
    public interface ICurrencyConvertService
    {
        CurrencyExchange GetExchangeResult(CurrencyExchange currencyExchange);
    }
}
