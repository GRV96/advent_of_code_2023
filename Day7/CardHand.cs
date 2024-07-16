namespace AoC2023
{
    internal class CardHand : IComparable<CardHand>
    {
        public const char JOKER_CHAR = 'X';
        public const int SIZE = 5;

        private CardLabel[] _cards;
        private CardHandType _type;
        private readonly int _bid;

        public int Bid
        {
            get
            {
                return _bid;
            }
        }

        public CardHandType Type
        {
            get
            {
                return _type;
            }
        }

        public CardHand(CardLabel[] pCards, int pBid)
        {
            _bid = pBid;
            _cards = new CardLabel[SIZE];
            Array.Copy(pCards, _cards, SIZE);
            DetermineHandType();
        }

        public int CompareTo(CardHand? pOther)
        {
            if (pOther == null)
            {
                return 1;
            }

            int typeComparison = _type - pOther._type;
            if (typeComparison != 0)
            {
                return typeComparison;
            }

            int labelComparison = 0;

            for (int i = 0; i < CardHand.SIZE; i++)
            {
                CardLabel cardLabel = _cards[i];
                CardLabel otherCardLabel = pOther._cards[i];
                labelComparison = cardLabel - otherCardLabel;

                if (labelComparison != 0)
                {
                    break;
                }
            }

            return labelComparison;
        }

        private void DetermineHandType()
        {
            CardLabel firstLabel = _cards[0];
            Dictionary<CardLabel, int> labelCounts = GetLabelCounts();
            if (labelCounts.TryGetValue(CardLabel.LX, out int nbJokers) && nbJokers > 0)
            {
                if (nbJokers == SIZE || nbJokers == SIZE - 1)
                {
                    _type = CardHandType.FIVE_KIND;
                    return;
                }

                // Pretend the jokers are the label with the highest count.
                GetHighestCountLabel(labelCounts, false, out CardLabel highestCountLabel, out int highestLabelCount);
                labelCounts[highestCountLabel] += nbJokers;
                labelCounts.Remove(CardLabel.LX);

                if (firstLabel == CardLabel.LX)
                {
                    firstLabel = highestCountLabel;
                }
            }

            int nbLabelCounts = labelCounts.Count;
            int firstLabelCount = labelCounts[firstLabel];

            if (nbLabelCounts == 1 && firstLabelCount == SIZE)
            {
                _type = CardHandType.FIVE_KIND;
            }
            else if (nbLabelCounts == 2)
            {
                if (firstLabelCount == 1 || firstLabelCount == SIZE - 1)
                {
                    _type = CardHandType.FOUR_KIND;
                }
                else if (firstLabelCount == 2 || firstLabelCount == SIZE - 2)
                {
                    _type = CardHandType.FULL_HOUSE;
                }
            }
            else if (nbLabelCounts == 3)
            {
                bool containsATrio = false;
                foreach (int labelCount in labelCounts.Values)
                {
                    if (labelCount == 3)
                    {
                        containsATrio = true;
                        break;
                    }
                }

                _type = containsATrio ? CardHandType.THREE_KIND : CardHandType.TWO_PAIR;
            }
            else if (nbLabelCounts == 4)
            {
                _type = CardHandType.ONE_PAIR;
            }
            else if (nbLabelCounts == 5)
            {
                _type = CardHandType.HIGH_CARD;
            }
        }

        private static bool GetHighestCountLabel(
            Dictionary<CardLabel, int> pLabelCounts, bool pIncludeJoker,
            out CardLabel pHighestCountLabel, out int pHighestLabelCount)
        {
            bool wasHighestCountFound = false;
            pHighestCountLabel = CardLabel.UNDEFINED;
            pHighestLabelCount = int.MinValue;

            foreach (KeyValuePair<CardLabel, int> kvp in pLabelCounts)
            {
                CardLabel cardLabel = kvp.Key;
                int labelCount = kvp.Value;

                if (!pIncludeJoker && cardLabel == CardLabel.LX)
                {
                    continue;
                }

                if (labelCount > pHighestLabelCount)
                {
                    pHighestCountLabel = cardLabel;
                    pHighestLabelCount = labelCount;
                    wasHighestCountFound = true;
                }
            }

            return wasHighestCountFound;
        }

        private Dictionary<CardLabel, int> GetLabelCounts()
        {
            Dictionary<CardLabel, int> labelCounts = new Dictionary<CardLabel, int>();

            foreach (CardLabel cardLabel in _cards)
            {
                int labelCount;
                if (labelCounts.TryGetValue(cardLabel, out labelCount))
                {
                    labelCounts[cardLabel] = labelCount + 1;
                }
                else
                {
                    labelCounts[cardLabel] = 1;
                }
            }

            return labelCounts;
        }

        public CardLabel GetCardLabel(int pIndex)
        {
            return _cards[pIndex];
        }

        public bool SubstituteJoker(CardLabel pCardLabel, out CardHand pNewCardHand)
        {
            bool originalCardContainsJokers = false;
            CardLabel[] cards = new CardLabel[SIZE];
            Array.Copy(_cards, cards, SIZE);

            for (int i = 0; i < SIZE; i++)
            {
                if (cards[i] == CardLabel.LX)
                {
                    cards[i] = pCardLabel;
                    originalCardContainsJokers = true;
                }
            }

            pNewCardHand = new CardHand(cards, _bid);
            return originalCardContainsJokers;
        }
    }
}
