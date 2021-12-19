using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CryptoWorldReview.Models
{
    class ModelCurrencyNBP
    {
        public string Currency { get; set; }
        public string Code { get; set; }
        public JObject[] Rates { get; set; }
    }
}
