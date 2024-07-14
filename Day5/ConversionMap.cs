namespace Day5
{
    internal class ConversionMap
    {
        public readonly string title;
        private readonly ConversionRange[] _conversionRanges;

        public ConversionMap(string pTitle, ConversionRange[] pConversionRanges)
        {
            title = pTitle;

            int nbRanges = pConversionRanges.Length;
            _conversionRanges = new ConversionRange[nbRanges];
            Array.Copy(pConversionRanges, _conversionRanges, nbRanges);
        }

        public int Convert(int pSource)
        {
            int destination = pSource;

            foreach (ConversionRange range in _conversionRanges)
            {
                if (range.Convert(pSource, out destination))
                {
                    break;
                }
            }

            return destination;
        }
    }
}
