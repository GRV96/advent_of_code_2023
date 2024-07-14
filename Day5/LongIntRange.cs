using Microsoft.VisualBasic;

namespace AoC2023
{
    internal class LongIntRange
    {
        // Both bounds are inclusive.
        public readonly long lowerSourceBound;
        public readonly long upperSourceBound;

        public LongIntRange(long pLowerSourceBound, long pUpperSourceBound)
        {
            lowerSourceBound = pLowerSourceBound;
            upperSourceBound = pUpperSourceBound;
        }

        public bool IsNumberIncluded(long pSource)
        {
            return pSource >= lowerSourceBound && pSource <= upperSourceBound;
        }
    }
}
