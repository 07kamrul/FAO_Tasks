using FAO_Tasks.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace FAO_Tasks
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello FAO:");
            Console.WriteLine("Press 1 for First Indicators");
            Console.WriteLine("Press 2 for Advanced Indicators");
            Console.WriteLine("Press 3 for Corrupted Indicators");

            string path1 = @"D:\Practice\ASA\FAO_Tasks\FAO_Tasks\InputData\data_cases_1.csv";
            
            string path2 = @"D:\Practice\ASA\FAO_Tasks\FAO_Tasks\InputData\data_cases_corrupted.csv";

            string diseasePath = @"D:\Practice\ASA\FAO_Tasks\FAO_Tasks\InputData\disease_list.csv";

            Console.WriteLine("\n\nEnter your needed indicator: ");
            int indicators = int.Parse(Console.ReadLine());

            switch (indicators)
            {
                case 1:
                    Console.WriteLine("First Indicators value are ready.");
                    FirstIndicators firstIndicators = new FirstIndicators();
                    firstIndicators.FirstIndicator(path1, indicators);
                    break;
                
                case 2:
                    Console.WriteLine("Advanced Indicators value are ready.");
                    AdvancedIndicators advancedIndicators = new AdvancedIndicators();
                    advancedIndicators.AdvancedIndicator(path1, diseasePath);
                    break;

                case 3:
                    Console.WriteLine("First Corrupted value are ready.");
                    CorruptedIndicators corruptedIndicators = new CorruptedIndicators();
                    corruptedIndicators.CorruptedIndicator(path2, indicators);
                    break;
                default:
                    Console.WriteLine(String.Format("Unknown indicators: {0}", indicators));

                    break;
            }

        }
    }
}
