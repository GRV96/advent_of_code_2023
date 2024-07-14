namespace Day5
{
    internal class ConversionRange
    {
        // Both bounds are inclusive.
        private readonly int _lowerSourceBound;
        private readonly int _upperSourceBound;

        private readonly int _conversion;

        public ConversionRange(int pLowerSourceBound, int pUpperSourceBound, int pConversion)
        {
            _lowerSourceBound = pLowerSourceBound;
            _upperSourceBound = pUpperSourceBound;

            _conversion = pConversion;
        }

        private bool IsSourceIncluded(int pSource)
        {
            return pSource >= _lowerSourceBound && pSource <= _upperSourceBound;
        }

        public bool Convert(int pSource, out int pDestination)
        {
            bool isSourceWithinRange = IsSourceIncluded(pSource);
            pDestination = isSourceWithinRange ? pSource + _conversion : pSource;
            return isSourceWithinRange;
        }
    }
}
