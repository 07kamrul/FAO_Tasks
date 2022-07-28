using FAO_Tasks.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace FAO_Tasks
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello FAO:");
            Console.WriteLine("Press 1 First Indicators");
            Console.WriteLine("Press 2 Advanced Indicators");
            Console.WriteLine("Press 3 Corrupted Indicators");

            string path1 = @"F:\Interview\FAO_Tasks\InputData\data_cases_1.csv";

            string path2 = @"F:\Interview\FAO_Tasks\InputData\data_cases_2.csv";

            Console.WriteLine("Enter your needed indicator: ");
            int indicators = int.Parse(Console.ReadLine());

            switch (indicators)
            {
                case 1:
                    Console.WriteLine("First Indicators value are ready.");
                    FirstIndicators firstIndicators = new FirstIndicators();
                    firstIndicators.FirstIndicator(path1);
                    break;

                default:
                    Console.WriteLine(String.Format("Unknown indicators: {0}", indicators));

                    break;
            }
            
        }
    }
}
