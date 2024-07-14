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
            List<LongIntRange> seedIdRangesPuzzle2 = null;

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
                        seedIdRangesPuzzle2 = ParseSeedIdRangesPuzzle2(line);
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

            long FindLocationIdFromSeedId(long pSeedId)
            {
                long locationId = pSeedId;

                foreach (ConversionMap conversionMap in conversionMaps.Values)
                {
                    locationId = conversionMap.Convert(locationId);
                }

                return locationId;
            }

            long nearestLocationIdPuzzle1 = long.MaxValue;
            foreach (long seedId in seedIdsPuzzle1)
            {
                long locationId = FindLocationIdFromSeedId(seedId);

                if (locationId < nearestLocationIdPuzzle1)
                {
                    nearestLocationIdPuzzle1 = locationId;
                }
            }

            long nearestLocationIdPuzzle2 = long.MaxValue;
            foreach (LongIntRange seedIdRange in seedIdRangesPuzzle2)
            {
                for (long i = seedIdRange.lowerBound; i <= seedIdRange.upperBound; i++)
                {
                    long locationId = FindLocationIdFromSeedId(i);

                    if (locationId < nearestLocationIdPuzzle2)
                    {
                        nearestLocationIdPuzzle2 = locationId;
                    }
                }
            }

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

        private static List<LongIntRange> ParseSeedIdRangesPuzzle2(string pSeedLine)
        {
            List<LongIntRange> seedIdRanges = new List<LongIntRange>();
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
                LongIntRange seedIdRange = new LongIntRange(lowerBound, upperBound);
                seedIdRanges.Add(seedIdRange);
            }

            return seedIdRanges;
        }
    }
}
