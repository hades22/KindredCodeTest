To quick start the web api application please click start button in visual studio.
The exchange rates are based on the response of ExchangeRate-API. The rates are cached until the time of the value of time_next_update_utc.
If the exchange rate between input and output currencies cannot be found from ExchangeRate-API the response will be in http 400 status.
Improvement to do: cache all the currencies exchange rates for input currency in one api call to ExchangeRate-API.
