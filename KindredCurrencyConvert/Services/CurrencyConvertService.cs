using KindredCurrencyConvert.Domain;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace KindredCurrencyConvert.Services
{
    public class CurrencyConvertService : ICurrencyConvertService
    {
        private readonly IConfiguration configuration;
        public CurrencyConvertService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public CurrencyExchange GetExchangeResult(CurrencyExchange currencyExchange)
        {
            var rate = GetExchangeRate(currencyExchange.InputCurrency, currencyExchange.OutputCurrency);
            if(rate.Status == Status.Ok)
            {
                return new CurrencyExchange()
                {
                    InputCurrency = currencyExchange.InputCurrency,
                    OutputCurrency = currencyExchange.OutputCurrency,
                    Amount = currencyExchange.Amount,
                    ConvertedAmount = rate.Rate * currencyExchange.Amount,
                    Status = Status.Ok
                };
            }
            else
            {
                return new CurrencyExchange()
                {
                    InputCurrency = currencyExchange.InputCurrency,
                    OutputCurrency = currencyExchange.OutputCurrency,
                    Amount = currencyExchange.Amount,
                    ConvertedAmount = null,
                    Status = rate.Status
                };
            }
        }

        private MemoryCache GetCache()
        {
            return MemoryCache.Default;
        }

        private ExchangeRate GetExchangeRate(string fromCurrency, string toCurrency)
        {
            var cache = GetCache();
            var key = $"{fromCurrency}-{toCurrency}";
            if (cache.Contains(key))
            {
                var rate = (ExchangeRate)cache[key];
                var now = DateTime.UtcNow;
                if(rate.ValidUntil > now)
                {
                    return rate;
                }
            }
            RefreshExchangeRate(fromCurrency, toCurrency);
            if (!cache.Contains(key))
            {
                return new ExchangeRate()
                {
                    Status = Status.ExchangeRateNotFound
                };
            }
            return (ExchangeRate)cache[key];
        }

        private void RefreshExchangeRate(string fromCurrency, string toCurrency)
        {
            var url = configuration.GetValue<string>("BaseApiUrl");
            var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = client.GetAsync(fromCurrency).Result;
            if (response.IsSuccessStatusCode)
            {
                var dataString = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<dynamic>(dataString);
                if (data["result"] == "success")
                {
                    var rates = data.rates;
                    var cache = GetCache();
                    var rateInfo = rates.GetType().GetProperty(toCurrency);
                    var rate = rates[toCurrency];
                    if (rate != null)
                    {
                        var exchangeRate = new ExchangeRate()
                        {
                            FromCurrency = fromCurrency,
                            ToCurrency = toCurrency,
                            Rate = rate,
                            ValidUntil = (DateTime)data.time_next_update_utc,
                            Status = Status.Ok
                        };

                        var cacheKey = $"{fromCurrency}-{toCurrency}";
                        CacheItemPolicy policy = new CacheItemPolicy();
                        policy.AbsoluteExpiration = DateTimeOffset.Now.AddHours(24);
                        cache.Set(cacheKey, exchangeRate, policy);
                    }
                }
            }
            
            client.Dispose();
        }
    }
}
