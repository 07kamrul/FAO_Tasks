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
            /*string path1 = @"D:\Practice\ASA\FAO_Tasks\FAO_Tasks\data\data_cases_1.csv";
            string path2 = @"D:\Practice\ASA\FAO_Tasks\FAO_Tasks\data\data_cases_2.csv";
*/
            string path1 = @"F:\Interview\FAO_Tasks\data\data_cases_1.csv";
            string path2 = @"F:\Interview\FAO_Tasks\data\data_cases_2.csv";

            string[] paths = new string[] { path1, path2 };

            List <DataCases> dataCases = new List<DataCases>();

            string[] rows;
            string[] columns;

            foreach (var path in paths) 
            {
                rows = File.ReadAllLines(path);
                
                for (int i = 1; i < rows.Length; i++)
                {
                    DataCases dc = new DataCases();

                    columns = rows[i].Split(',');
                    dc.uuid = Encoding.Unicode.GetBytes(columns[0]);
                    dc.datetime = Convert.ToDateTime(columns[1]);
                    dc.species = columns[2];
                    dc.number_morbidity = Convert.ToInt32(columns[3]);
                    dc.disease_id = Convert.ToInt32(columns[4]);
                    dc.number_mortality = Convert.ToInt32(columns[5]);
                    dc.total_number_cases = Convert.ToInt32(columns[6]);
                    dc.location = columns[7];

                    dataCases.Add(dc);
                }
            }

            Dictionary<string, List<DataCases>> casesDictionary = dataCases.GroupBy(x => x.location).ToDictionary(x => x.Key, x => x.ToList());
      
            foreach (var item in casesDictionary)
            {
                Console.WriteLine("Number of Locations: " + item.Key+" : "+item.Value.Count());

            }

        }
    }

    public class DataCases
    {
        public byte[] uuid { get; set; }
        public DateTime datetime { get; set; }
        public string species { get; set; }
        public int number_morbidity { get; set; }
        public int disease_id { get; set; }
        public int number_mortality { get; set; }
        public int total_number_cases { get; set; }
        public string location { get; set; }
    }

    public class Disease
    {
        public int id { get; set; }
        public String name { get; set; }
    }
}
