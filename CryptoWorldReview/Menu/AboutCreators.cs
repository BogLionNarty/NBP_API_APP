using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoWorldReview.Menu
{
    class AboutCreators
    {
        public void DisplayInfoAboutCreators()
        {
            var root = new Tree("POLITECHNIKA BIAŁOSTOCKA");
            var foo = root.AddNode("[yellow]Rok 2021/2022[/]").AddNode("[yellow]Semestr 5[/]").AddNode("[yellow]PS1[/]");
            var table = foo.AddNode(new Table()
                .RoundedBorder()
                .AddColumn("Imię")
                .AddColumn("Nazwisko")
                .AddRow("Łukasz", "Godlewski")
                .AddRow("Cezary", "Bączek"));
            AnsiConsole.Write(root);
        }
    }
}
