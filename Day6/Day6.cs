namespace AoC2023
{
    internal class Day6
    {
        private const char SPACE = ' ';

        private const string TITLE_TIME = "Time:";
        private const string TITLE_DISTANCE = "Distance:";

        static void Main(string[] args)
        {
            string inputPath = args[0];

            List<int> times = null;
            List<int> distances = null;

            using (StreamReader reader = new StreamReader(inputPath))
            {
                string? line = null;
                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim();
                    string lineTitle;
                    List<int> numbers = ParseRaceRecordLine(line, out lineTitle);

                    switch(lineTitle)
                    {
                        case TITLE_TIME:
                            times = numbers;
                            break;
                        case TITLE_DISTANCE:
                            distances = numbers;
                            break;
                    }
                }
            }

            int nbRecords = times.Count;
            List<RaceRecord> raceRecords = new List<RaceRecord>();
            for (int i = 0; i < nbRecords; i++)
            {
                raceRecords.Add(new RaceRecord(times[i], distances[i]));
            }

            int nbSucessesProduct = 1;
            foreach (RaceRecord raceRecord in raceRecords)
            {
                int nbSuccesses = TryToBreakRaceRecord(raceRecord);
                nbSucessesProduct *= nbSuccesses;
            }

            Console.WriteLine("Puzzle 1: " + nbSucessesProduct);
        }

        private static List<int> ParseRaceRecordLine(string pRaceRecordLine, out string pLineTitle)
        {
            List<int> numbers = new List<int>();

            string[] rawNumbers = pRaceRecordLine.Split(SPACE);
            int nbRawNumbers = rawNumbers.Length;
            pLineTitle = rawNumbers[0];

            for (int i = 1; i < nbRawNumbers; i++)
            {
                int rawNumber;
                if (int.TryParse(rawNumbers[i], out rawNumber))
                {
                    numbers.Add(rawNumber);
                }
            }

            return numbers;
        }

        private static int TryToBreakRaceRecord(RaceRecord pRaceRecord)
        {
            int nbSuccesses = 0;

            int allowedTime = pRaceRecord.allowedTime;
            int recordDistance = pRaceRecord.distance;
            for (int speed = 1; speed < allowedTime; speed++)
            {
                // The speed is equal to the time that the button has been pressed for.
                int distanceTraveled = speed * (allowedTime - speed);

                if (distanceTraveled > recordDistance)
                {
                    nbSuccesses++;
                }
            }

            return nbSuccesses;
        }
    }
}
