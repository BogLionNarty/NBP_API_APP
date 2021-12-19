using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoWorldReview.Charts
{
    class BarCharts
    {
        public BarCharts(List<(string, double)> items)
        {
            items = items.Where(x => x.Item2 != 0).ToList();
            AnsiConsole.Write(new BarChart().Width(60).CenterLabel().AddItems(items, (item) => new BarChartItem(item.Item1, item.Item2, Color.Blue)));
        }
    }
}
