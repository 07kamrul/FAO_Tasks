﻿using System;
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
            string path1 = @"D:\Practice\ASA\FAO_Tasks\FAO_Tasks\data\data_cases_1.csv";
            string path2 = @"D:\Practice\ASA\FAO_Tasks\FAO_Tasks\data\data_cases_2.csv";
            
            string[] paths = new string[] { path1, path2 };

            List <DataCases> dataCases = new List<DataCases>();

            string[] rows;
            string[] columns;

            foreach (var path in paths) 
            {
                rows = File.ReadAllLines(path);
                int i = 1;
                DataCases dc = new DataCases();
                while (i < rows.Length)
                {
                    columns = rows[i].Split(',');
                    if (columns.Length > 0)
                    {
                    }
                    dc.uuid = Encoding.Unicode.GetBytes(columns[0]);
                    dc.datetime = Convert.ToDateTime(columns[1]);
                    dc.species = columns[2];
                    dc.number_morbidity = Convert.ToInt32(columns[3]);
                    dc.disease_id = Convert.ToInt32(columns[4]);
                    dc.number_mortality = Convert.ToInt32(columns[5]);
                    dc.total_number_cases = Convert.ToInt32(columns[6]);
                    dc.location = columns[7];

                    i++;
                    dataCases.Add(dc);
                }


/*                Dictionary<string, DataCases> casesDictionary = dataCases.ToDictionary(x => x.location);
                int total = dataCases.Count;
                Console.WriteLine("Number of Locations: " + total);
                foreach (var item in casesDictionary.Keys)
                {

                }*/
            }
            Console.WriteLine(dataCases.Count());


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
