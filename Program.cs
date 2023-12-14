using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;


namespace BPAPrep1
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            List<AgeData> list = new List<AgeData>();
            string FPath = "Sample_Data_Age.csv";

            string SeniorFPath = "Seniors.csv";
            string AgeData = "age_data.csv";
            string AlphaAll = "alpha_all.csv";

            using (var reader = new StreamReader(FPath))
            using (var csv = new CsvReader(reader,CultureInfo.InvariantCulture))
            {
                var Records = csv.GetRecords<DataClass>().ToList();

                //Console.WriteLine(Records.Count);

                var Over64 = Records.Where(r => int.Parse(r.Age) > 64);

                Console.Write(Over64.Count());

                using (var writer = new StreamWriter(SeniorFPath))
                using (var csvWriter = new  CsvWriter(writer,CultureInfo.InvariantCulture))
                {
                    csvWriter.WriteRecords(Over64);
                }

                var AlphaRecords = Records.OrderBy(r => r.Name);

                using (var writer = new StreamWriter(AlphaAll))
                using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csvWriter.WriteRecords(AlphaRecords);
                }

                var From18To24 = Records.Where(r => int.Parse(r.Age) >= 18 && int.Parse(r.Age) < 25);

                list.Add(new AgeData { AgeCount = From18To24.Count(), AgeRange = "18-24" });

                var From25To35 = Records.Where(r => int.Parse(r.Age) >= 25 && int.Parse(r.Age) < 36);

                list.Add(new AgeData { AgeCount = From25To35.Count(), AgeRange = "25-35" });

                var From36To55 = Records.Where(r => int.Parse(r.Age) >= 36 && int.Parse(r.Age) < 55);

                list.Add(new AgeData { AgeCount = From36To55.Count(), AgeRange = "36-55" });

                var From55AndAbove = Records.Where(r => int.Parse(r.Age) > 55);

                list.Add(new AgeData { AgeCount = From55AndAbove.Count(), AgeRange = "55+" });

                using (var writer = new StreamWriter(AgeData))
                using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csvWriter.WriteRecords(list);
                }

            }

        }
    }
}
