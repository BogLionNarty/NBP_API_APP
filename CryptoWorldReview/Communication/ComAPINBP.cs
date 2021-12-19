using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CryptoWorldReview.ConstValues;
using CryptoWorldReview.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Spectre.Console;

namespace CryptoWorldReview.Communication
{
    static class ComAPINBP
    {
        private static readonly HttpClient client = new();

        //InitializeClient().GetAwaiter().GetResult();

        public static async Task InitializeClient()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //DisplayToConsoleDebug(await GetData(ISOCurrencyCodes.USD));
            //DisplayToConsoleDebug(await GetData(ISOCurrencyCodes.EUR, new DateTime(2015,05,05)));
            //DisplayToConsoleDebug(await GetData(ISOCurrencyCodes.EUR, new DateTime(2015,05,05), new DateTime(2015, 06, 05)));
        }

        private static void DisplayToConsoleDebug(ModelCurrencyNBP model)
        {
            AnsiConsole.WriteLine();
            AnsiConsole.WriteLine(model.Currency);
            AnsiConsole.WriteLine(model.Code);
            foreach (JObject item in model.Rates)
            {
                ModelRatesNBP rates = JsonConvert.DeserializeObject<ModelRatesNBP>(item.ToString());
                AnsiConsole.WriteLine(rates.No);
                AnsiConsole.WriteLine(rates.Ask);
                AnsiConsole.WriteLine(rates.Bid);
            }
        }

        private static async Task<ModelCurrencyNBP> GetDataFromURL(string url)
        {
            using HttpResponseMessage response = await client.GetAsync(url);
            return response.IsSuccessStatusCode ? await response.Content.ReadAsAsync<ModelCurrencyNBP>() : new ModelCurrencyNBP { Code = "N/A", Currency = "N/A" };
        }

        /// <param name="code">Currency code</param>
        /// <returns>One last element of today</returns>
        public static async Task<ModelCurrencyNBP> GetData(ISOCurrencyCodes code) => await GetDataFromURL($"http://api.nbp.pl/api/exchangerates/rates/c/{code}/today/?format=json");

        /// <param name="code">Currency code</param>
        /// <param name="count">Number of last elements</param>
        /// <returns>List of last elements</returns>
        public static async Task<ModelCurrencyNBP> GetData(ISOCurrencyCodes code, int count) => await GetDataFromURL($"http://api.nbp.pl/api/exchangerates/rates/c/{code}/last/{count}/?format=json");

        /// <param name="code">Currency code</param>
        /// <param name="fromDate">Date from which get element from</param>
        /// <returns>One element from date</returns>
        public static async Task<ModelCurrencyNBP> GetData(ISOCurrencyCodes code, DateTime fromDate) => await GetDataFromURL($"http://api.nbp.pl/api/exchangerates/rates/c/{code}/{fromDate:yyyy-MM-dd}/?format=json");

        /// <param name="code">Currency code</param>
        /// <param name="fromDate">Date from which get element from</param>
        /// <returns>One element from date</returns>
        public static async Task<ModelCurrencyNBP> GetData(ISOCurrencyCodes code, DateTime fromDate, DateTime toDate) => await GetDataFromURL($"http://api.nbp.pl/api/exchangerates/rates/c/{code}/{fromDate:yyyy-MM-dd}/{toDate:yyyy-MM-dd}/?format=json");
    }
}
