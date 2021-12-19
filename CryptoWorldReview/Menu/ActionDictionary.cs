using CryptoWorldReview.ConstValues;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoWorldReview.Menu
{
    public class ActionDictionary
    {
        private readonly string[] listOfCurrency = Enum.GetValues(typeof(ISOCurrencyCodes)).OfType<Enum>().Select(o => o.ToString()).ToArray();
        private readonly Dictionary<int, string> dict = new();
        public ActionDictionary()
        {
            Create();
        }
        public void Create()
        {
            for (int i = 1; i < listOfCurrency.Length; i++)
                dict.Add(i, listOfCurrency[i]);
        }

        public string ReturnCode(int i)
        {
            string value = "";
            dict.TryGetValue(i, out value);
            return value;
        }
    }
}
