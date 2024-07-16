namespace AoC2023
{
    internal class Day7
    {
        private const char SPACE = ' ';

        static void Main(string[] args)
        {
            string inputPath = args[0];

            int nbCardHands = 0;
            List<CardHand> cardHandsPuzzle1 = new List<CardHand>();
            List<CardHand> cardHandsPuzzle2 = new List<CardHand>();

            using (StreamReader reader = new StreamReader(inputPath))
            {
                string? line = null;
                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim();
                    CardHand cardHandPuzzle1 = ParseCardHandPuzzle1(line);
                    CardHand cardHandPuzzle2 = ParseCardHandPuzzle2(line);
                    cardHandsPuzzle1.Add(cardHandPuzzle1);
                    cardHandsPuzzle2.Add(cardHandPuzzle2);
                    nbCardHands++;
                }
            }

            cardHandsPuzzle1.Sort();
            cardHandsPuzzle2.Sort();
            int totalWinningsPuzzle1 = 0;
            int totalWinningsPuzzle2 = 0;
            for (int i = 0; i < nbCardHands; i++)
            {
                totalWinningsPuzzle1 += (i + 1) * cardHandsPuzzle1[i].Bid;
                totalWinningsPuzzle2 += (i + 1) * cardHandsPuzzle2[i].Bid;
            }

            Console.WriteLine("Puzzle 1: " + totalWinningsPuzzle1);
            Console.WriteLine("Puzzle 2: " + totalWinningsPuzzle2);
        }

        private static CardHand ParseCardHandPuzzle1(string pCardHandLine)
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

        private static CardHand ParseCardHandPuzzle2(string pCardHandLine)
        {
            char[] cardHandSymbols = pCardHandLine.ToCharArray();

            int lineLength = cardHandSymbols.Length;
            for (int i = 0; i < lineLength; i++)
            {
                if (cardHandSymbols[i] == 'J')
                {
                    cardHandSymbols[i] = CardHand.JOKER_CHAR;
                }
            }

            return ParseCardHandPuzzle1(new string(cardHandSymbols));
        }

        private static CardLabel ParseCardLabel(char pCardLabelSymbol)
        {
            switch(pCardLabelSymbol)
            {
                case CardHand.JOKER_CHAR:
                    return CardLabel.LX;
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
