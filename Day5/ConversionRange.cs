namespace Day5
{
    internal class ConversionRange
    {
        // Both bounds are inclusive.
        private readonly Int64 _lowerSourceBound;
        private readonly Int64 _upperSourceBound;

        private readonly Int64 _conversion;

        public ConversionRange(Int64 pLowerSourceBound, Int64 pUpperSourceBound, Int64 pConversion)
        {
            _lowerSourceBound = pLowerSourceBound;
            _upperSourceBound = pUpperSourceBound;

            _conversion = pConversion;
        }

        private bool IsSourceIncluded(Int64 pSource)
        {
            return pSource >= _lowerSourceBound && pSource <= _upperSourceBound;
        }

        public bool Convert(Int64 pSource, out Int64 pDestination)
        {
            bool isSourceWithinRange = IsSourceIncluded(pSource);
            pDestination = isSourceWithinRange ? pSource + _conversion : pSource;
            return isSourceWithinRange;
        }
    }
}
