using System;

namespace FinancialSharp.CSharp.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var nper = Financial.Nper(0.075, -2000.0, 0.0, 100000.0, PaymentDuePeriod.Begin);
            Console.WriteLine($"Number of periodic payments: {nper}");

            var fv = Financial.Fv(0.075, 20.0, -2000.0, 0.0, PaymentDuePeriod.End);
            Console.WriteLine($"Future value: {fv}");

            var npv = Financial.Npv(0.05, new[] { -15000.0, 1500.0, 2500.0, 3500.0, 4500.0, 6000.0 });
            Console.WriteLine($"Net present value of a cash flow series: {npv}");

            var pv = Financial.Pv(0.0, 20.0, 12000.0, 0.0);
            Console.WriteLine($"Present value: {pv}");
        }
    }
}
