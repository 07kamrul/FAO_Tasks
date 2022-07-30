using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAO_Tasks.Models
{
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
}
