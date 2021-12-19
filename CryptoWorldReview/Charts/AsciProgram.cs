using System;
using System.Linq;
using System.Text;
using Spectre.Console;

namespace AsciiChart.Sharp.TestApp
{
    class AsciProgram
    {
        public AsciProgram(double[] numbers)
        {
            Console.SetCursorPosition(0, 20);
            numbers = numbers.Except(new double[] { 0 }).ToArray();
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine(AsciiChart.Plot(numbers, new Options
            {
                AxisLabelLeftMargin = 0,
                AxisLabelRightMargin = 2,
                Height = 10,
                Fill = '·',
                AxisLabelFormat = "0.00000",
            }));
            Console.WriteLine();
            AnsiConsole.Write(new Rule().RuleStyle("blue dim"));
        }
    }
}
