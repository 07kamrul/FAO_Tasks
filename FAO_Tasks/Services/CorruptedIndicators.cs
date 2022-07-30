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
    public class CorruptedIndicators
    {
        public void CorruptedIndicator(string rootPath, int indicators)
        {
            List<DataCases> dataCases = new List<DataCases>();

            string[] rows;
            string[] corruptedColumns;
            string path = rootPath + @"\InputData\data_cases_corrupted.csv";

            rows = File.ReadAllLines(path);

            for (int i = 1; i < rows.Length; i++)
            {
                DataCases dc = new DataCases();
                string[] columns = new string[8];

                corruptedColumns = rows[i].Split(',');
                if (corruptedColumns.Length > 8)
                {
                    for (int l = 0;  l < corruptedColumns.Length; l++)
                    {
                        int columnsPosCounter = 0;

                        if (corruptedColumns[l].Contains(":"))
                        {
                            for (int a = 0; a < l; a++)
                            {
                                columns[columnsPosCounter] += corruptedColumns[a]; 
                            }
                            columns[0] = columns[columnsPosCounter].Replace("\"ABB", "");
                            columns[1] = corruptedColumns[l];

                            for (int c = l; c < corruptedColumns.Length; c++)
                            {
                                columnsPosCounter++;
                                columns[columnsPosCounter] = corruptedColumns[c];  
                            }                            
                        }                        
                    }
                }
                else
                {
                    columns = corruptedColumns;
                }                

                dc.uuid = Encoding.Unicode.GetBytes(columns[0]);
                dc.datetime = Convert.ToDateTime(columns[1]);
                dc.species = columns[2];
                dc.number_morbidity = Convert.ToInt32(columns[3]);
                dc.disease_id = Convert.ToInt32(columns[4]);
                dc.number_mortality = Convert.ToInt32(columns[5]);
                dc.total_number_cases = Convert.ToInt32(columns[6]);
                dc.location = columns[7];

                dataCases.Add(dc);

                CommonService commonService = new CommonService();
                commonService.JsonCommonService(dataCases, indicators, rootPath);
            }
        }
    }
}
