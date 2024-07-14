namespace AoC2023
{
    internal struct ScratchcardRepository
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
}
