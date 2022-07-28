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
            /*string path1 = @"D:\Practice\ASA\FAO_Tasks\FAO_Tasks\data\data_cases_1.csv";
            string path2 = @"D:\Practice\ASA\FAO_Tasks\FAO_Tasks\data\data_cases_2.csv";
*/
            string path1 = @"F:\Interview\FAO_Tasks\InputData\data_cases_1.csv";
            //string path2 = @"F:\Interview\FAO_Tasks\InputData\data_cases_2.csv";

            string[] paths = new string[] { path1 };

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


            var location_name = dataCases.Select(x => new { location = x.location }).GroupBy(g => g.location).ToList();

            IDictionary<string, object> jsonDictionary = new Dictionary<string, object>();
            jsonDictionary.Add(new KeyValuePair<string, object>("total number of reported cases is" , dataCases.Sum(x => x.total_number_cases)));

            IDictionary<string, int> d = new Dictionary<string, int>();


            foreach (var ln in location_name.OrderBy(x=> x.Key))
            {
                string key = ln.Key;
                int value = dataCases.Where(x => x.location == ln.Key).Sum(a => a.number_mortality);

                d.Add(new KeyValuePair<string, int>(ln.Key, value));

            }

            Console.WriteLine("total number of reported cases is: "+dataCases.Sum(x => x.total_number_cases));
            foreach (KeyValuePair<string, int> ele in d)
            {
                Console.WriteLine("{0} : {1}", ele.Key, ele.Value);
            }

            jsonDictionary.Add(new KeyValuePair<string, object>("total number of deaths reported at each location", d));


            string json = JsonSerializer.Serialize(jsonDictionary);
            File.WriteAllText(@"F:\Interview\FAO_Tasks\OutputData\indicators_1.json", json);
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
