using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KindredCurrencyConvert.Domain;
using KindredCurrencyConvert.Model.Request;
using KindredCurrencyConvert.Model.Response;
using KindredCurrencyConvert.Services;
using Microsoft.AspNetCore.Mvc;

namespace KindredCurrencyConvert.Controllers
{
    [Route("api/")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyConvertService currencyConvertService;
        public CurrencyController(ICurrencyConvertService currencyConvertService)
        {
            this.currencyConvertService = currencyConvertService;
        }

        [Route("ExchangeService")]
        [HttpPost]
        public ActionResult<CurrencyConvertResponse> Exchange(CurrencyConvertRequest request)
        {
            var currencyExchange = new CurrencyExchange()
            {
                InputCurrency = request.InputCurrency,
                OutputCurrency = request.OutputCurrency,
                Amount = request.Amount,
                ConvertedAmount = null
            };

            var result = currencyConvertService.GetExchangeResult(currencyExchange);
            if (result.Status == Status.Ok)
            {
                var response = new CurrencyConvertResponse()
                {
                    Amount = result.Amount,
                    InputCurrency = result.InputCurrency,
                    OutputCurrency = result.OutputCurrency,
                    Value = result.ConvertedAmount.Value
                };
                return Ok(response);
            }
            return BadRequest("Exchange rate not found");
        }

    }
}
