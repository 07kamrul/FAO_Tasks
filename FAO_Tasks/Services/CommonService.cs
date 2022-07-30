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
    public class CommonService
    {
       
        public void JsonCommonService(List<DataCases> dataCases, int indicator)
        {
            var location_name = dataCases.Select(x => new { location = x.location }).GroupBy(g => g.location).ToList();

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

            if (indicator == 1)
            {
                File.WriteAllText(@"D:\Practice\ASA\FAO_Tasks\FAO_Tasks\OutputData\indicators_1.json", jsonIndicators);
            }
            else
            {
                File.WriteAllText(@"D:\Practice\ASA\FAO_Tasks\FAO_Tasks\OutputData\indicators_corrupted.json", jsonIndicators);
            }
        }
    }
}
