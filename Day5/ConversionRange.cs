using AoC2023;

namespace Day5
{
    internal class ConversionRange : LongIntRange
    {
        private readonly long _conversion;

        public ConversionRange(long pLowerSourceBound, long pUpperSourceBound, long pConversion)
            : base(pLowerSourceBound, pUpperSourceBound)
        {
            _conversion = pConversion;
        }

        public bool Convert(long pSource, out long pDestination)
        {
            bool isNumberWithinRange = IsNumberIncluded(pSource);
            pDestination = isNumberWithinRange ? pSource + _conversion : pSource;
            return isNumberWithinRange;
        }
    }
}
