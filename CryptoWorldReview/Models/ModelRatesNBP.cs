using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoWorldReview.Models
{
    class ModelRatesNBP
    {
        public string No { get; set; }
        public string EffectiveDate { get; set; }
        public double Bid { get; set; }
        public double Ask { get; set; }
    }
}
