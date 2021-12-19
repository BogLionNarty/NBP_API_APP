using AsciiChart.Sharp.TestApp;
using CryptoWorldReview.Communication;
using CryptoWorldReview.ConstValues;
using CryptoWorldReview.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using CryptoWorldReview.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoWorldReview.Controllers
{
    class DisplayChartsController
    {
        private readonly int DaysToDisplay = 90;
        public DisplayChartsController() => ComAPINBP.InitializeClient().GetAwaiter().GetResult();
        public async void DisplayAsciChart(ISOCurrencyCodes code, DateTime endDate)
        {
            _ = new AsciProgram(ConvertModelCurrencyNBPToDoubleArray(await ComAPINBP.GetData(code, endDate.AddDays(-DaysToDisplay), endDate)));
        }

        public async void DisplayBarChart(ISOCurrencyCodes code, DateTime endDate)
        {
            List<(string, double)> lista = new();
            double[] arr = ConvertModelCurrencyNBPToDoubleArray(await ComAPINBP.GetData(code, endDate.AddDays(-DaysToDisplay), endDate));
            for (int i = 0; i < DaysToDisplay; i++)
                lista.Add((endDate.AddDays(-(DaysToDisplay - i)).ToString("dd-MM-yyyy"), arr[i]));
            _ = new BarCharts(lista);
        }

        public double DisplayValueOnDate(ISOCurrencyCodes code, DateTime date) => ConvertModelCurrencyNBPToDouble(ComAPINBP.GetData(code, date).Result);

        private double[] ConvertModelCurrencyNBPToDoubleArray(ModelCurrencyNBP model)
        {   
            double[] tab = new double[DaysToDisplay];
            for (int i = 0; i < model.Rates.Length; i++)
            {
                JObject item = model.Rates[i];
                ModelRatesNBP rates = JsonConvert.DeserializeObject<ModelRatesNBP>(item.ToString());
                tab[i] = rates.Ask;
            }
            return tab;
        }
        private double ConvertModelCurrencyNBPToDouble(ModelCurrencyNBP model)
        {
            for (int i = 0; i < model.Rates.Length; i++)
            {
                JObject item = model.Rates[i];
                ModelRatesNBP rates = JsonConvert.DeserializeObject<ModelRatesNBP>(item.ToString());
                return rates.Ask;
            }
            return 0;
        }
    }
}
