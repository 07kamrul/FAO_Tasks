using FAO_Tasks.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
namespace FAO_Tasks.Services
{
    class FirstIndicators
    {
        public void FirstIndicator(string path)
        {

            List<DataCases> dataCases = new List<DataCases>();

            string[] rows;
            string[] columns;

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

            var location_name = dataCases.Select(x => new { location = x.location })
                .GroupBy(g => g.location).ToList();

            IDictionary<string, object> indicators = new Dictionary<string, object>();
            IDictionary<string, int> location = new Dictionary<string, int>();

            indicators.Add(
                new KeyValuePair<string, object>
                (
                    "total number of reported cases is",
                    dataCases.Sum(x => x.total_number_cases)
                )
            );

            foreach (var ln in location_name.OrderBy(x => x.Key))
            {
                int numberOfDeaths = dataCases.Where(x => x.location == ln.Key)
                    .Sum(a => a.number_mortality);

                location.Add(
                    new KeyValuePair<string, int>
                    (
                        ln.Key, 
                        numberOfDeaths
                        )
                    );
            }

            indicators.Add(new KeyValuePair<string, object>("total number of deaths reported at each location", location));

            string jsonIndicators = JsonSerializer.Serialize(indicators);
            File.WriteAllText(@"F:\Interview\FAO_Tasks\OutputData\indicators_1.json", jsonIndicators);
        }
    }
}
