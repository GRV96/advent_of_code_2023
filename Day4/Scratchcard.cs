namespace AoC2023
{
    internal class Scratchcard
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
}
