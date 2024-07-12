using System.Collections.Generic;

namespace AoC2023
{
    class Day1
    {
        private static string[] DIGITS_AS_STRINGS = [
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "zero",
            "one",
            "two",
            "three",
            "four",
            "five",
            "six",
            "seven",
            "eight",
            "nine"
        ];

        private static Dictionary<string, int> STRING_TO_DIGIT = new Dictionary<string, int>()
        {
            {"0", 0},
            {"1", 1},
            {"2", 2},
            {"3", 3},
            {"4", 4},
            {"5", 5},
            {"6", 6},
            {"7", 7},
            {"8", 8},
            {"9", 9},
            {"zero", 0},
            {"one", 1},
            {"two", 2},
            {"three", 3},
            {"four", 4},
            {"five", 5},
            {"six", 6},
            {"seven", 7},
            {"eight", 8},
            {"nine", 9}
        };

        private static int ParsePuzzle1Line(string pPuzzleLine)
        {
            char firstDigit = '0';
            foreach(char ch in pPuzzleLine)
            {
                if (Char.IsDigit(ch))
                {
                    firstDigit = ch;
                    break;
                }
            }

            char lastDigit = '0';
            for (int i = pPuzzleLine.Length - 1; i >= 0; i--)
            {
                char ch = pPuzzleLine[i];

                if (Char.IsDigit(ch))
                {
                    lastDigit = ch;
                    break;
                }
            }

            int number = int.Parse(new string([firstDigit, lastDigit]));

            return number;
        }

        private static int ParsePuzzle2Line(string pPuzzleLine)
        {
            int firstDigit = -1;
            int firstDigitIndex = int.MaxValue;
            int lastDigit = -1;
            int lastDigitIndex = -1;

            foreach (string digitAsStr in DIGITS_AS_STRINGS)
            {
                int digitIndex = pPuzzleLine.IndexOf(digitAsStr);
                if (digitIndex >= 0 && digitIndex < firstDigitIndex)
                {
                    firstDigitIndex = digitIndex;
                    firstDigit = STRING_TO_DIGIT[digitAsStr];
                }

                digitIndex = pPuzzleLine.LastIndexOf(digitAsStr);
                if (digitIndex >= 0 && digitIndex > lastDigitIndex)
                {
                    lastDigitIndex = digitIndex;
                    lastDigit = STRING_TO_DIGIT[digitAsStr];
                }
            }

            return 10 * firstDigit + lastDigit;
        }

        public static void Main(string[] args)
        {
            string inputPath = args[0];

            List<int> numbers = DataReading.ParseDataFromFile<int>(inputPath, ParsePuzzle1Line);

            int sum = 0;
            foreach (int number in numbers)
            {
                sum += number;
            }

            Console.WriteLine("Puzzle 1: " + sum);

            numbers = DataReading.ParseDataFromFile<int>(inputPath, ParsePuzzle2Line);

            sum = 0;
            foreach (int number in numbers)
            {
                sum += number;
            }

            Console.WriteLine("Puzzle 2: " + sum);
        }
    }
}
