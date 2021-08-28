using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Application
{
    class Program
    {
        public static double Calculate(string userInput)
        {
            var parts = userInput.Split();
            var sum = parts[0];
            int sumCredit = int.Parse(sum);
            var rate = parts[1];
            double procent = double.Parse(rate);
            var time = parts[2];
            int timeCredit = int.Parse(time);
            procent = (procent / 100) + 1;
            double res;
            res = sumCredit * (Math.Pow(procent, timeCredit));
            return res;
        }
        static void Main()
        {
            Console.Write("ввод суммы, процентной ставки и срока:");
            string userInput = Console.ReadLine();
            Console.WriteLine(Calculate(userInput));
        }
    }
}
