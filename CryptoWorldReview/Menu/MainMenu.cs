using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
using ConsoleTables;
using AsciiChart;
using AsciiChart.Sharp.TestApp;
using CryptoWorldReview.Charts;
using CryptoWorldReview.ConstValues;
using System.Threading;
using CryptoWorldReview.Controllers;

namespace CryptoWorldReview.Menu
{
    public class MainMenu
    {

        private readonly string[] mainMenu = { "1. Sprawdź kurs", "2. Sprawdź wykres", "3. Wyczyść ekran", "4. O api", "5. O twórcach", "6. Wyjście" };
        private readonly string[] chartsMenu = { "1. Wykres liniowy", "2. Wykres słupkowy", "3. Powrót" };
        private readonly string[] listOfCurrency = Enum.GetValues(typeof(ISOCurrencyCodes)).OfType<Enum>().Select(o => o.ToString()).ToArray();
        private readonly ActionDictionary dict = new();
        private readonly DisplayChartsController displayChartsController = new();
        private readonly AboutAPI aboutAPI = new();
        private readonly AboutCreators aboutCreators = new();
        public MainMenu()
        {
            Console.OutputEncoding = Encoding.Default;
            Console.SetCursorPosition(0, 9);
   
            int choice = MultipleChoice(true, 1, mainMenu);
            switch (choice)
            {
                case 1:
                    CourseMenu();
                    break;
                case 2:
                    ChartMenu();
                    break;
                case 3:
                    ClearMenuWithoutBanner();
                    new MainMenu();
                    break;
                case 4:
                    aboutAPI.DisplayInfoAboutApi();
                    new MainMenu();
                    break;
                case 5:
                    aboutCreators.DisplayInfoAboutCreators();
                    new MainMenu();
                    break;
            }
        }

        public int MultipleChoice(bool canCancel, int optionPerLine, params string[] options)
        {
            const int startX = 0;
            const int startY = 9;
            int optionsPerLine = optionPerLine;
            const int spacingPerLine = 14;
            int currentSelection = 0;
            ConsoleKey key;
            Console.CursorVisible = false;
            do
            {
                Console.SetCursorPosition(0, 7);
                AnsiConsole.Write(new Rule().RuleStyle("blue dim"));
                for (int i = 0; i < options.Length; i++)
                {
                    Console.SetCursorPosition(startX + (i % optionsPerLine) * spacingPerLine, startY + i / optionsPerLine);

                    if (i == currentSelection)
                        Console.ForegroundColor = ConsoleColor.Blue;

                    Console.Write(options[i]);
                    Console.ResetColor();
                }
                int a = Int32.Parse(Console.CursorTop.ToString());
                Console.SetCursorPosition(0, a + 2);
                AnsiConsole.Write(new Rule().RuleStyle("blue dim"));
                key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.LeftArrow:
                        if (currentSelection % optionsPerLine > 0)
                            currentSelection--;
                        break;
                    case ConsoleKey.RightArrow:
                        if (currentSelection % optionsPerLine < optionsPerLine - 1)
                            currentSelection++;
                        break;
                    case ConsoleKey.UpArrow:
                        if (currentSelection >= optionsPerLine)
                            currentSelection -= optionsPerLine;
                        break;
                    case ConsoleKey.DownArrow:
                        if (currentSelection + optionsPerLine < options.Length)
                            currentSelection += optionsPerLine;
                        break;
                    case ConsoleKey.Escape:
                        if (canCancel)
                            return -1;
                        break;
                }
            } while (key != ConsoleKey.Enter);
            Console.CursorVisible = true;
            return currentSelection + 1;
        }

        public void CourseMenu()
        {
            Console.SetCursorPosition(0, 9);
            int year = YearMenu();
            int month = MonthMenu();
            Calendar(month, year);
            int day = DayMenu();
            DateTime date = SetTheDate(year, month, day);
            int currency = MultipleChoice(true, 10, listOfCurrency);
            ISOCurrencyCodes code = Enum.Parse<ISOCurrencyCodes>(Enum.GetName(typeof(ISOCurrencyCodes), currency - 1));
            double value = displayChartsController.DisplayValueOnDate(code, date);
            DisplayStatus(false);
            SettingsDisplayCourse(day, month, year, code.ToString(), value);
            new MainMenu();
        }

        public void ChartMenu()
        {
            ClearMenuWithoutBanner();
            int choice = MultipleChoice(true, 1, chartsMenu);
            Console.SetCursorPosition(0, 9);
            int year = YearMenu();
            int month = MonthMenu();
            Calendar(month, year);
            int day = DayMenu();
            DateTime date = SetTheDate(year, month, day);
            ClearMenuWithoutBanner();
            switch (choice)
            {
                case 1:
                    int linear = MultipleChoice(true, 10, listOfCurrency);
                    ClearMenuWithoutBanner();
                    Console.SetCursorPosition(0, 9);
                    DisplayStatus(true);
                    ClearMenuWithoutBanner();
                    Console.SetCursorPosition(0, 17);
                    ISOCurrencyCodes code = Enum.Parse<ISOCurrencyCodes>(Enum.GetName(typeof(ISOCurrencyCodes), linear - 1));
                    string a = ((date.AddDays(-100).ToString("dd/MM/yyyy")) + " - " + date.ToString("dd/MM/yyyy") + " " + "PLN/" + code.ToString());
                    var table = new Table();
                    table.AddColumn(a);
                    table.Border = TableBorder.Rounded;
                    AnsiConsole.Write(table);
                    Console.SetCursorPosition(0, 19);
                    displayChartsController.DisplayAsciChart(code, date);
                    new MainMenu();
                    break;
                case 2:
                    int bar = MultipleChoice(true, 10, listOfCurrency);
                    ClearMenuWithoutBanner();
                    Console.SetCursorPosition(0, 9);
                    DisplayStatus(true);
                    ClearMenuWithoutBanner();
                    Console.SetCursorPosition(0, 17);
                    ISOCurrencyCodes code2 = Enum.Parse<ISOCurrencyCodes>(Enum.GetName(typeof(ISOCurrencyCodes), bar - 1));
                    string b = ((date.AddDays(-100).ToString("dd/MM/yyyy")) + " - " + date.ToString("dd/MM/yyyy") + " " + "PLN/" + code2.ToString());
                    var table1 = new Table();
                    table1.AddColumn(b);
                    table1.Border = TableBorder.Rounded;
                    AnsiConsole.Write(table1);
                    Console.SetCursorPosition(0, 19);
                    displayChartsController.DisplayBarChart(code2, date);
                    new MainMenu();
                    break;
                case 3:
                    new MainMenu();
                    break;
            }
        }

        public static void ClearMenuWithoutBanner()
        {
            for (int i = 9; i < 33; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(new string(' ', Console.WindowWidth));
            }
        }

        public static void Calendar(int month, int year)
        {
            AnsiConsole.Write(new Calendar(year, month).HeaderStyle(Style.Parse("blue bold")));
            Console.WriteLine();
        }

        public static void DisplayRule(string month, int year, bool justify)
        {
            var rule = new Rule(month + " " + year);
            rule.RuleStyle("blue dim");
            if (justify == true)
            {
                rule.Alignment = Justify.Left;
                AnsiConsole.Write(rule);
            }
            else
                AnsiConsole.Write(rule);
        }

        public static void DisplayStatus(bool isChart)
        {
            if(isChart == true)
            {
                AnsiConsole.Status()
                .Start("Myślę...", ctx =>
                {
                AnsiConsole.MarkupLine("Pytam Glapińskiego o zgodę na połączenie do API...");
                Thread.Sleep(1000);
                ctx.Status("Rysuje wykres na podstawie wartości z 90 dni wstecz od podanej daty.");
                ctx.Spinner(Spinner.Known.Star);
                ctx.SpinnerStyle(Style.Parse("green"));
                AnsiConsole.MarkupLine("Zgoda uzyskana.");
                 Thread.Sleep(5000);
                });
            }
            else
            {
                AnsiConsole.Status()
               .Start("Myślę...", ctx =>
               {
                   AnsiConsole.MarkupLine("Pytam Glapińskiego o zgodę na połączenie do API...");
                   Thread.Sleep(1000);
                   ctx.Status("Pobieram dane.");
                   ctx.Spinner(Spinner.Known.Star);
                   ctx.SpinnerStyle(Style.Parse("green"));
                   AnsiConsole.MarkupLine("Zgoda uzyskana.");
                   Thread.Sleep(5000);
               });
            }
        }

        public static int YearMenu()
        {
            MainMenu.ClearMenuWithoutBanner();
            Console.SetCursorPosition(0, 9);
            int year = AnsiConsole.Prompt(
            new TextPrompt<int>("Podaj rok, z którego chcesz pobrać dane?")
            .Validate(year =>
            {
                return year switch
                {
                    < 2015 => ValidationResult.Error("[red]Brak danych w api z tego roku![/]"),
                    > 2022 => ValidationResult.Error("[red]Brak danych w api z tego roku![/]"),
                    _ => ValidationResult.Success(),
                };
            }));
            return year;
        }
        public static int MonthMenu()
        {

            //ClearMenuWithoutBanner();
            //Console.SetCursorPosition(0, 9);
            int month = AnsiConsole.Prompt(
            new TextPrompt<int>("Podaj miesiąc, z którego chcesz pobrać dane?")
            .Validate(month =>
            {
                return month switch
                {
                    < 1 => ValidationResult.Error("[red]Podaj prawidłowy miesiąc![/]"),
                    > 12 => ValidationResult.Error("[red]Podaj prawidłowy miesiąc![/]"),
                    _ => ValidationResult.Success(),
                };
            }));
            Console.WriteLine();
            return month;
        }
        public static int DayMenu()
        {
            int day = AnsiConsole.Prompt(
            new TextPrompt<int>("Podaj dzień, z którego chcesz pobrać dane? Dla ułatwienia został wyświetlony kalendarz. A następnie wybierz walutę: ")
            .Validate(day =>
            {
                return day switch
                {
                    < 1 => ValidationResult.Error("[red]Zerknij na kalendarz i wpisz poprawny dzień![/]"),
                    > 31 => ValidationResult.Error("[red]Zerknij na kalendarz i wpisz poprawny dzień![/]"),
                    _ => ValidationResult.Success(),
                };
            }));
            MainMenu.ClearMenuWithoutBanner();
            return day;

        }
        public static void SettingsDisplayCourse(int finalDay, int finalMonth, int finalYear, string finalCurrency, double finalValue)
        {
            Console.SetCursorPosition(0, 9);
            MainMenu.ClearMenuWithoutBanner();
            var table = new Table();
            Console.SetCursorPosition(0, 9);
            table.AddColumn("ROK").Centered();
            table.AddColumn("MIESIĄC").Centered();
            table.AddColumn("DZIEŃ").Centered();
            table.AddColumn("WALUTA").Centered();
            table.AddColumn("WARTOŚĆ").Centered();
            table.AddRow(new Panel(finalYear.ToString()), new Panel(finalMonth.ToString()), new Panel(finalDay.ToString()), new Panel(finalCurrency), new Panel(finalValue.ToString()));
            AnsiConsole.Write(table);
        }

        private DateTime SetTheDate(int year, int month, int day)
        {
            DateTime datetime = new(year, month, day);
            return datetime;
        }
    }
}

