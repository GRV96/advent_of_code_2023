namespace Day5
{
    internal class ConversionRange
    {
        // Both bounds are inclusive.
        private readonly long _lowerSourceBound;
        private readonly long _upperSourceBound;

        private readonly long _conversion;

        public ConversionRange(long pLowerSourceBound, long pUpperSourceBound, long pConversion)
        {
            _lowerSourceBound = pLowerSourceBound;
            _upperSourceBound = pUpperSourceBound;

            _conversion = pConversion;
        }

        private bool IsSourceIncluded(long pSource)
        {
            return pSource >= _lowerSourceBound && pSource <= _upperSourceBound;
        }

        public bool Convert(long pSource, out long pDestination)
        {
            bool isSourceWithinRange = IsSourceIncluded(pSource);
            pDestination = isSourceWithinRange ? pSource + _conversion : pSource;
            return isSourceWithinRange;
        }
    }
}
