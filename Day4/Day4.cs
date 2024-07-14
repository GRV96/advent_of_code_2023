namespace AoC2023
{
    internal class Day4
    {
        private const char COLON = ':';
        private const string PIPE = "|";
        private const char SPACE = ' ';

        static void Main(string[] args)
        {
            string inputPath = args[0];
            ScratchcardRepository scratchcardRepository = new ScratchcardRepository();
            int scratchcardPointSum = 0;

            using (StreamReader reader = new StreamReader(inputPath))
            {
                string? line = null;
                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim();
                    Scratchcard scratchcard = ParseScartchcardData(line);
                    scratchcardRepository.AddScratchcard(scratchcard);
                    scratchcardPointSum += scratchcard.NbPoints;
                }
            }

            scratchcardRepository.CopyScratchcards();

            Console.WriteLine("Puzzle 1: " + scratchcardPointSum);
            Console.WriteLine("Puzzle 2: " + scratchcardRepository.CountScratchcards());
        }

        private static bool ParseDataItem(string pDataItem, out int pNumber, out bool pIsScratchcardId)
        {
            bool isParsingSuccessful = false;

            int dataItemLastIndex = pDataItem.Length - 1;
            if (pDataItem[dataItemLastIndex] == COLON)
            {
                pIsScratchcardId = true;
                isParsingSuccessful = int.TryParse(pDataItem.Substring(0, dataItemLastIndex), out pNumber);
            }
            else
            {
                pIsScratchcardId = false;
                isParsingSuccessful = int.TryParse(pDataItem, out pNumber);
            }

            return isParsingSuccessful;
        }

        private static Scratchcard ParseScartchcardData(string pScratchcardData)
        {
            int scratchCardId = 0;
            List<int> winningNumbers = new List<int>();
            List<int> numbers = new List<int>();
            List<int> listToFill = winningNumbers;

            string[] dataItems = pScratchcardData.Split(SPACE);
            foreach (string dataItem in dataItems)
            {
                if (dataItem.Length == 0)
                {
                    continue;
                }

                if (dataItem == PIPE)
                {
                    listToFill = numbers;
                    continue;
                }

                int number;
                bool isNumberScratchCardId;
                bool isParsingSuccessful = ParseDataItem(dataItem, out number, out isNumberScratchCardId);

                if (!isParsingSuccessful)
                {
                    continue;
                }

                if (isNumberScratchCardId)
                {
                    scratchCardId = number;
                }
                else
                {
                    listToFill.Add(number);
                }
            }

            return new Scratchcard(scratchCardId, winningNumbers.ToArray(), numbers.ToArray());
        }
    }
}
