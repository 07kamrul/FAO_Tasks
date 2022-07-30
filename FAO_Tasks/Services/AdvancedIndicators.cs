using FAO_Tasks.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FAO_Tasks.Services
{
    class AdvancedIndicators
    {
        public void AdvancedIndicator(string path, string diseasePath)
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

            var disease_ids = dataCases.Select(x => new { disease_id = x.disease_id })
                .GroupBy(g => g.disease_id).OrderBy(k => k.Key).ToList();

            //disease list
            List<Disease> diseases = new List<Disease>();
            rows = File.ReadAllLines(diseasePath);
            for (int i = 1; i < rows.Length; i++)
            {
                Disease disease = new Disease();

                columns = rows[i].Split(',');
                disease.Id = Convert.ToInt32(columns[0]);
                disease.Name = columns[1];

                diseases.Add(disease);
            }

            int numberOfDiseas = diseases.Count();

            foreach (var disease_id in disease_ids)
            {
                var abc = diseases.Where(d => d.Id == disease_id.Key);
            }

            IDictionary<string, object> indicators = new Dictionary<string, object>();
            IDictionary<string, int> listOfDieases = new Dictionary<string, int>();

            var catAvg = dataCases.Where(s => s.species == "cat" && s.location.Contains("Village")).Average(x => x.number_morbidity);

            indicators.Add(
                new KeyValuePair<string, object>
                (
                    "Average number of sick cats reported in reports from villages up to two decimal points",
                    Math.Round(catAvg, 2, MidpointRounding.ToEven)
                )
            );

            foreach (var ln in disease_ids)
            {
                listOfDieases.Add(
                    new KeyValuePair<string, int>
                    (
                        diseases.Where(d => d.Id == ln.Key).FirstOrDefault().Name,
                        dataCases.Where(x => x.disease_id == ln.Key).Sum(a => a.number_mortality)
                        )
                    );
            }

            IDictionary<string, int> outputDieases = new Dictionary<string, int>();
            foreach (var ln in listOfDieases.OrderBy(l => l.Key))
            {
                outputDieases.Add(
                    new KeyValuePair<string, int>
                    (
                        ln.Key,
                        ln.Value
                        )
                    );
            }

            indicators.Add(new KeyValuePair<string, object>("total number of deaths from each disease", outputDieases));

            string jsonIndicators = JsonSerializer.Serialize(indicators);
            File.WriteAllText(@"D:\Practice\ASA\FAO_Tasks\FAO_Tasks\OutputData\indicators_advanced.json", jsonIndicators);
        }
    }
}
