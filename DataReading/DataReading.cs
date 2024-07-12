using System.IO;
using System.Collections.Generic;

namespace AoC2023
{
    public class DataReading
    {
        public static List<string> GetLinesFromFile(string pFilePath)
        {
            List<string> lines = new List<string>();

            using(StreamReader reader = new StreamReader(pFilePath))
            {
                string? line = null;

                while((line = reader.ReadLine()) != null)
                {
                    line = line.Trim();
                    lines.Add(line);
                }
            }

            return lines;
        }

        public static List<T> ParseDataFromFile<T>(string pFilePath, Func<string, T> pParsing)
        {
            List<T> allData = new List<T>();

            using (StreamReader reader = new StreamReader(pFilePath))
            {
                string? line = null;

                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim();
                    T parsedData = pParsing(line);
                    allData.Add(parsedData);
                }
            }

            return allData;
        }
    }
}
