using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AmberCastle.Cbr.CbrWebServ;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using WebExchangeRates.Models;

namespace WebExchangeRates.Services
{
    public class CurrencyService : BackgroundService
    {
        #region Поля

        private readonly IMemoryCache _memoryCache;
        private readonly DailyInfoClient _client;

        #endregion

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _memoryCache.Set("key_currency", GetCurrency(), TimeSpan.FromMinutes(1440));

                await Task.Delay(3600000, stoppingToken);
            }
        }

        private  CurrencyConverter GetCurrency()
        {
            CurrencyConverter currencyConverter = new CurrencyConverter();
            try
            {
                var lastData = _client.GetLatestDate().Result;
                var cursOnDate = _client.GetCursOnDate(lastData).Result;
                currencyConverter.USD = (decimal)cursOnDate.FirstOrDefault(x => x.Vcode == 840).Vcurs;
                currencyConverter.EUR = (decimal)cursOnDate.FirstOrDefault(x => x.Vcode == 978).Vcurs;
                currencyConverter.UAN = (decimal)cursOnDate.FirstOrDefault(x => x.Vcode == 980).Vcurs;
            }
            catch (Exception e)
            {
                // logs......
            }
            
            return currencyConverter;
        }

        #region Конструктор

        public CurrencyService(IMemoryCache memoryCache, DailyInfoClient client)
        {
            _memoryCache = memoryCache;
            _client = client;
            System.Threading.Thread.Sleep(5000);
        }

        #endregion
    }
}
