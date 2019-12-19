using banks.Model;
using System;

namespace banks
{
    class Program
    {
        static void Main(string[] args)
        {
            decimal Amount = 100;
            decimal Percent = (decimal)7.5;
            decimal a = Amount / Percent;
            
            decimal sum = Amount * (1 + (Percent / 100));
            Console.WriteLine(sum);
        }
    }
}
