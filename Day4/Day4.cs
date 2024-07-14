namespace AoC2023
{
    internal class Day4
    {
        private const char COLON = ':';
        private const string PIPE = "|";
        private const char SPACE = ' ';

        private class Scratchcard
        {
            public readonly int id;

            private int[] _winningNumbers;
            private int[] _numbers;

            private int _nbCopiesOwned = 1;
            private int _nbMatches;
            private int _nbPoints;

            public int NbCopiesOwned
            {
                get
                {
                    return _nbCopiesOwned;
                }
                set
                {
                    _nbCopiesOwned = value;
                }
            }

            public int NbMatches
            {
                get
                {
                    return _nbMatches;
                }
            }

            public int NbPoints
            {
                get
                {
                    return _nbPoints;
                }
            }

            public Scratchcard(int pId, int[] pWinningNumbers, int[] pNumbers)
            {
                id = pId;

                int nbWinningNumbers = pWinningNumbers.Length;
                _winningNumbers = new int[nbWinningNumbers];
                Array.Copy(pWinningNumbers, _winningNumbers, nbWinningNumbers);

                int nbNumbers = pNumbers.Length;
                _numbers = new int[nbNumbers];
                Array.Copy(pNumbers, _numbers, nbNumbers);

                CalculateMatchesAndPoints();
            }

            private void CalculateMatchesAndPoints()
            {
                _nbMatches = 0;

                foreach (int number in _numbers)
                {
                    if (_winningNumbers.Contains(number))
                    {
                        _nbMatches++;
                    }
                }

                if (_nbMatches > 0)
                {
                    _nbPoints = (int)Math.Pow(2, _nbMatches - 1);
                }
            }
        }

        private struct ScratchcardRepository
        {
            private Dictionary<int, Scratchcard> _content;

            public ScratchcardRepository()
            {
                _content = new Dictionary<int, Scratchcard>();
            }

            public void AddScratchcard(Scratchcard pScratchcard)
            {
                _content[pScratchcard.id] = pScratchcard;
            }

            public void CopyScratchcards()
            {
                foreach (Scratchcard scratchcard in _content.Values)
                {
                    int scratchcardId = scratchcard.id;
                    int nbCopiesOwned = scratchcard.NbCopiesOwned;
                    int nbMatches = scratchcard.NbMatches;
                    int idBound = scratchcardId + nbMatches;

                    for (int i = scratchcardId + 1; i <= idBound; i++)
                    {
                        _content[i].NbCopiesOwned += nbCopiesOwned;
                    }
                }
            }

            public int CountScratchcards()
            {
                int nbScratchcards = 0;

                foreach (Scratchcard scratchcard in _content.Values)
                {
                    nbScratchcards += scratchcard.NbCopiesOwned;
                }

                return nbScratchcards;
            }

            public Scratchcard GetScratchcard(int pScratchcardId)
            {
                return _content[pScratchcardId];
            }
        }

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
