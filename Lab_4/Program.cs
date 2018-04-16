using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_4
{
    class Program
    {
        static void Main(string[] args)
        {
            long start = DateTime.Now.Ticks;

            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine($"{i} Result: " + new Calculator("Math.Pow(x * 2 + Math.PI + Math.Sin(x * 3 * y), 3)")
                    .Calc(1.5, 0.17));
            }

            long end1 = DateTime.Now.Ticks - start;

            long start2 = DateTime.Now.Ticks;
            var calculator = new Calculator("Math.Pow(x * 2 + Math.PI + Math.Sin(x * 3 * y), 3)");

            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine($"{i} Result: " + calculator.Calc(1.5, 0.17));
            }

            long end2 = DateTime.Now.Ticks - start2;

            Console.WriteLine($"End 1: {end1}");
            Console.WriteLine($"End 2: {end2}");
            Console.WriteLine($"Diff: {end1 - end2}");
        }
    }
}
