using Spectre.Console;
using System;
using CryptoWorldReview.View;
using CryptoWorldReview.Communication;

namespace CryptoWorldReview
{
    class Program
    {
        private static MainView mainView = new();
        static void Main(string[] args)
        {
            mainView.Display(); 
            //ComAPINBP.InitializeClient().GetAwaiter().GetResult();
        }
    }
}
