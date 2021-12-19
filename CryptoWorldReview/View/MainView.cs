using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
using AsciiChart;
using System.Threading;
using CryptoWorldReview.ConstValues;

namespace CryptoWorldReview.View
{
    class MainView
    {
        public void Display()
        {
            Console.WindowWidth = 136;
            Console.SetWindowPosition(0, 0);
            Console.WriteLine(Properties.Resources.LOGO);
            _ = new Menu.MainMenu();
        }
    }
}