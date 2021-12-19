using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;

namespace CryptoWorldReview.Menu
{
    class AboutAPI
    {
        
        public void DisplayInfoAboutApi()
        {
            AnsiConsole.Write(
            new FigletText("Narodowy Bank Polski")
            .Centered()
            .Color(Color.Red));
            AnsiConsole.Write("Serwis api.nbp.pl udostępnia publiczne Web API umożliwiające klientom HTTP wykonywanie zapytań na poniższych zbiorach danych publikowanych przez serwis NBP.PL:" +
                "aktualne oraz archiwalne kursy walut obcych:" +
                "Tabela A kursów średnich walut obcych," +
                "Tabela B kursów średnich walut obcych," +
                "Tabela C kursów kupna i sprzedaży walut obcych;" +
                "aktualne oraz archiwalne ceny złota wyliczone w NBP." +
                "Komunikacja z serwisem polega na wysłaniu odpowiednio sparametryzowanego żądania HTTP GET na adres bazowy http://api.nbp.pl/api/");
        }
    }
}
