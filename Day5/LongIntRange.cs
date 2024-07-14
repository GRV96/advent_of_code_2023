namespace AoC2023
{
    internal class LongIntRange
    {
        // Both bounds are inclusive.
        public readonly long lowerBound;
        public readonly long upperBound;

        public LongIntRange(long pLowerBound, long pUpperBound)
        {
            lowerBound = pLowerBound;
            upperBound = pUpperBound;
        }

        public bool IsNumberIncluded(long pNumber)
        {
            return pNumber >= lowerBound && pNumber <= upperBound;
        }
    }
}
