using Day5;

namespace AoC2023
{
    internal class Day5
    {
        private const string SEEDS_COLON = "seeds:";
        private const string MAP_COLON = "map:";

        private const char SPACE = ' ';

        static void Main(string[] args)
        {
            string inputPath = args[0];

            List<int> seedIds = null;

            string mapTitle = string.Empty;
            List<ConversionRange> conversionRanges = new List<ConversionRange>();
            Dictionary<string, ConversionMap> conversionMaps = new Dictionary<string, ConversionMap>();

            void RecordConversionMap()
            {
                if (mapTitle.Length > 0 && conversionRanges.Count > 0)
                {
                    ConversionMap conversionMap = new ConversionMap(mapTitle, conversionRanges.ToArray());
                    conversionMaps[mapTitle] = conversionMap;

                    mapTitle = string.Empty;
                    conversionRanges.Clear();
                }
            }

            using (StreamReader reader = new StreamReader(inputPath))
            {
                string? line = null;
                while (true)
                {
                    line = reader.ReadLine();

                    if (line == null)
                    {
                        RecordConversionMap();
                        break;
                    }
                    else
                    {
                        line = line.Trim();
                    }

                    if (line.Length == 0)
                    {
                        RecordConversionMap();
                    }
                    else if (line.IndexOf(SEEDS_COLON) >= 0)
                    {
                        seedIds = ParseSeedIds(line);
                        continue;
                    }
                    else if (line.IndexOf(MAP_COLON) >= 0)
                    {
                        mapTitle = line.Split(SPACE)[0];
                    }
                    else
                    {
                        ConversionRange conversionRange = ParseConversionRange(line);
                        conversionRanges.Add(conversionRange);
                    }
                }
            }

            int nearestLocationId = int.MaxValue;
            foreach (int seedId in seedIds)
            {
                int destination = seedId;

                foreach (ConversionMap conversionMap in conversionMaps.Values)
                {
                    destination = conversionMap.Convert(destination);
                }

                if (destination < nearestLocationId)
                {
                    nearestLocationId = destination;
                }
            }

            Console.WriteLine("Puzzle 1: " + nearestLocationId);
        }

        private static ConversionRange ParseConversionRange(string pRangeData)
        {
            string[] dataItems = pRangeData.Split(SPACE);
            int lowerDestinationBound = int.Parse(dataItems[0]);
            int lowerSourceBound = int.Parse(dataItems[1]);
            int rangeLength = int.Parse(dataItems[2]);

            int upperSourceBound = lowerSourceBound + rangeLength - 1;
            int conversion = lowerDestinationBound - lowerSourceBound;
            return new ConversionRange(lowerSourceBound, upperSourceBound, conversion);
        }

        private static List<int> ParseSeedIds(string pSeedLine)
        {
            List<int> seedIds = new List<int>();
            string[] seedStrings = pSeedLine.Split(SPACE);

            foreach (string seedString in seedStrings)
            {
                if (int.TryParse(seedString, out int seedId))
                {
                    seedIds.Add(seedId);
                }
            }

            return seedIds;
        }
    }
}
