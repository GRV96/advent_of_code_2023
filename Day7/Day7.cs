namespace AoC2023
{
    internal class Day7
    {
        private const char SPACE = ' ';

        static void Main(string[] args)
        {
            string inputPath = args[0];

            List<CardHand> cardHands = new List<CardHand>();

            using (StreamReader reader = new StreamReader(inputPath))
            {
                string? line = null;
                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim();
                    CardHand cardHand = ParseCardHand(line);
                    cardHands.Add(cardHand);
                }
            }

            int nbCardHands = cardHands.Count;
            cardHands.Sort();
            int totalWinnings = 0;
            for (int i = 0; i < nbCardHands; i++)
            {
                totalWinnings += (i + 1) * cardHands[i].Bid;
            }

            Console.WriteLine("Puzzle 1: " + totalWinnings);
        }

        private static CardHand ParseCardHand(string pCardHandLine)
        {
            string[] splitLine = pCardHandLine.Split(SPACE);
            string cardData = splitLine[0];
            string bidData = splitLine[1];

            CardLabel[] cardLabels = new CardLabel[CardHand.SIZE];
            for (int i = 0; i < CardHand.SIZE; i++)
            {
                cardLabels[i] = ParseCardLabel(cardData[i]);
            }

            int bid = int.Parse(bidData);

            return new CardHand(cardLabels, bid);
        }

        private static CardLabel ParseCardLabel(char pCardLabelSymbol)
        {
            switch(pCardLabelSymbol)
            {
                case '2':
                    return CardLabel.L2;
                case '3':
                    return CardLabel.L3;
                case '4':
                    return CardLabel.L4;
                case '5':
                    return CardLabel.L5;
                case '6':
                    return CardLabel.L6;
                case '7':
                    return CardLabel.L7;
                case '8':
                    return CardLabel.L8;
                case '9':
                    return CardLabel.L9;
                case 'T':
                    return CardLabel.L10;
                case 'J':
                    return CardLabel.LJ;
                case 'Q':
                    return CardLabel.LQ;
                case 'K':
                    return CardLabel.LK;
                case 'A':
                    return CardLabel.LA;
                default:
                    return CardLabel.UNDEFINED;
            }
        }
    }
}
