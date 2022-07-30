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
        public void FirstIndicator(string rootPath, int indicators)
        {

            List<DataCases> dataCases = new List<DataCases>();

            string[] rows;
            string[] columns;
            string path = rootPath + @"\InputData\data_cases_1.csv";


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

            CommonService commonService = new CommonService();
            commonService.JsonCommonService(dataCases, indicators, rootPath);
        }
    }
}
