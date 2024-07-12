using System.IO;
using System.Collections.Generic;

namespace AoC2023
{
    internal class DataReading
    {
        public static List<String> GetLinesFromFile(String pFilePath)
        {
            List<String> lines = new List<String>();

            using(StreamReader reader = new StreamReader(pFilePath))
            {
                String? line = null;

                while((line = reader.ReadLine()) != null)
                {
                    line = line.Trim();
                    lines.Add(line);
                }
            }

            return lines;
        }

        public static List<T> ParseDataFromFile<T>(String pFilePath, Func<String, T> pParsing)
        {
            List<T> allData = new List<T>();

            using (StreamReader reader = new StreamReader(pFilePath))
            {
                String? line = null;

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
