using banks.Model;
using System;

namespace banks
{
    class Program
    {
        static void Main(string[] args)
        {
            decimal amount = 30000;
            decimal per = (decimal)5.5;
            int month = 10;
            var loanAmount = Math.Round(amount * (decimal)Math.Pow((double)per / 100 + 1, month), 2);
            Console.WriteLine(loanAmount);
        }
    }
}
