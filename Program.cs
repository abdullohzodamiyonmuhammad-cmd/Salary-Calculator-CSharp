using System;
using System.Globalization;
using System.Text;

namespace ConsoleApplication1
{
    internal class Program
    {
        private const double NormalHours = 160;
        private const double OvertimeRate = 1.5;

        private static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("=== Системаи ҳисобкунии маош ===");
            Console.WriteLine("Меъёр: " + NormalHours + " соат дар як моҳ.");
            Console.WriteLine("Соатҳои иловагӣ бо коэффитсиенти " + OvertimeRate + " ҳисоб мешаванд.");
            Console.WriteLine("Барои баромадан 'q' ворид кунед.");
            Console.WriteLine();

            while (true)
            {
                bool quit;

                double hours = ReadNumber("Соатҳои корӣ: ", 0, 744, out quit);
                if (quit) break;

                double rate = ReadNumber("Нархи як соат: ", 0, 1000000, out quit);
                if (quit) break;

                double regularHours;
                double overtimeHours;
                double regularPay;
                double overtimePay;

                double total = CalculateSalary(hours, rate, out regularHours, out overtimeHours,
                    out regularPay, out overtimePay);

                Console.WriteLine();
                Console.WriteLine("--- Ҳисоби маош ---");
                Console.WriteLine("Соатҳои оддӣ:      " + Format(regularHours) + " x " + Format(rate) + " = " + Format(regularPay));
                Console.WriteLine("Соатҳои иловагӣ:   " + Format(overtimeHours) + " x " + Format(rate * OvertimeRate) + " = " + Format(overtimePay));
                Console.WriteLine("Маоши умумӣ:       " + Format(total));
                Console.WriteLine();
            }

            Console.WriteLine("Хайр!");
        }

        private static double CalculateSalary(double hours, double rate,
            out double regularHours, out double overtimeHours,
            out double regularPay, out double overtimePay)
        {
            if (hours > NormalHours)
            {
                regularHours = NormalHours;
                overtimeHours = hours - NormalHours;
            }
            else
            {
                regularHours = hours;
                overtimeHours = 0;
            }

            regularPay = regularHours * rate;
            overtimePay = overtimeHours * rate * OvertimeRate;

            return regularPay + overtimePay;
        }

        private static double ReadNumber(string prompt, double min, double max, out bool quit)
        {
            quit = false;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                input = (input == null) ? "" : input.Trim();

                if (input.Equals("q", StringComparison.OrdinalIgnoreCase))
                {
                    quit = true;
                    return 0;
                }

                input = input.Replace(',', '.');

                double value;
                if (double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out value)
                    && value >= min && value <= max)
                {
                    return value;
                }

                Console.WriteLine("Хато: адад аз " + min + " то " + max + " ворид кунед.");
            }
        }

        private static string Format(double value)
        {
            return value.ToString("0.##", CultureInfo.InvariantCulture);
        }
    }
}