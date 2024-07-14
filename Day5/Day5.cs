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

            List<long> seedIdsPuzzle1 = null;
            List<long> seedIdsPuzzle2 = null;

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
                        seedIdsPuzzle1 = ParseSeedIdsPuzzle1(line);
                        seedIdsPuzzle2 = ParseSeedIdsPuzzle2(line);
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

            long FindNearestLocationId(List<long> pSeedIds)
            {
                long nearestLocationId = long.MaxValue;

                foreach (long seedId in pSeedIds)
                {
                    long destination = seedId;

                    foreach (ConversionMap conversionMap in conversionMaps.Values)
                    {
                        destination = conversionMap.Convert(destination);
                    }

                    if (destination < nearestLocationId)
                    {
                        nearestLocationId = destination;
                    }
                }

                return nearestLocationId;
            }

            long nearestLocationIdPuzzle1 = FindNearestLocationId(seedIdsPuzzle1);
            long nearestLocationIdPuzzle2 = FindNearestLocationId(seedIdsPuzzle2);

            Console.WriteLine("Puzzle 1: " + nearestLocationIdPuzzle1);
            Console.WriteLine("Puzzle 2: " + nearestLocationIdPuzzle2);
        }

        private static ConversionRange ParseConversionRange(string pRangeData)
        {
            string[] dataItems = pRangeData.Split(SPACE);
            long lowerDestinationBound = long.Parse(dataItems[0]);
            long lowerSourceBound = long.Parse(dataItems[1]);
            long rangeLength = long.Parse(dataItems[2]);

            long upperSourceBound = lowerSourceBound + rangeLength - 1;
            long conversion = lowerDestinationBound - lowerSourceBound;
            return new ConversionRange(lowerSourceBound, upperSourceBound, conversion);
        }

        private static List<long> ParseSeedIdsPuzzle1(string pSeedLine)
        {
            List<long> seedIds = new List<long>();
            string[] seedStrings = pSeedLine.Split(SPACE);

            foreach (string seedString in seedStrings)
            {
                if (long.TryParse(seedString, out long seedId))
                {
                    seedIds.Add(seedId);
                }
            }

            return seedIds;
        }

        private static List<long> ParseSeedIdsPuzzle2(string pSeedLine)
        {
            List<long> seedIds = new List<long>();
            string[] seedStrings = pSeedLine.Split(SPACE);

            int nbSeedStrings = seedStrings.Length;
            for (int i = 0; i < nbSeedStrings; i += 2)
            {
                long lowerBound;
                if (!long.TryParse(seedStrings[i], out lowerBound))
                {
                    i--;
                    continue;
                }

                long rangeLength;
                if (!long.TryParse(seedStrings[i + 1], out rangeLength))
                {
                    i--;
                    continue;
                }

                long upperBound = lowerBound + rangeLength - 1;

                // Both bounds are inclusive.
                for (long j = lowerBound; j <= upperBound; j++)
                {
                    seedIds.Add(j);
                }
            }

            return seedIds;
        }
    }
}
