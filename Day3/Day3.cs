namespace AoC2023
{
    internal class Day3
    {
        private const char ASTERISK = '*';
        private const char PERIOD = '.';

        private struct Gear
        {
            public readonly int lineIndex;
            public readonly int columnIndex;

            private HashSet<Number> _adjacentNumbers;

            public int NbAdjacentNumbers
            {
                get
                {
                    return _adjacentNumbers.Count;
                }
            }

            public Gear(int pLineIndex, int pColumnIndex)
            {
                lineIndex = pLineIndex;
                columnIndex = pColumnIndex;
                _adjacentNumbers = new HashSet<Number>();
            }

            public int CalculateGearRatio()
            {
                int nbAdjacentNumbers = NbAdjacentNumbers;
                if (nbAdjacentNumbers == 0)
                {
                    return 0;
                }

                int gearRatio = 1;
                foreach (Number number in _adjacentNumbers)
                {
                    gearRatio *= number.value;
                }
                return gearRatio;
            }

            public bool CheckIfNumberIsAdjacent(Number pNumber)
            {
                int prevLineIndex = lineIndex - 1;
                int nextLineIndex = lineIndex + 1;

                int prevColumnIndex = columnIndex - 1;
                int nextColumnIndex = columnIndex + 1;

                if (
                    pNumber.IncludesCoordinates(prevLineIndex, columnIndex)
                    || pNumber.IncludesCoordinates(prevLineIndex, nextColumnIndex)
                    || pNumber.IncludesCoordinates(lineIndex, nextColumnIndex)
                    || pNumber.IncludesCoordinates(nextLineIndex, nextColumnIndex)
                    || pNumber.IncludesCoordinates(nextLineIndex, columnIndex)
                    || pNumber.IncludesCoordinates(nextLineIndex, prevColumnIndex)
                    || pNumber.IncludesCoordinates(lineIndex, prevColumnIndex)
                    || pNumber.IncludesCoordinates(prevLineIndex, prevColumnIndex))
                {
                    _adjacentNumbers.Add(pNumber);
                    return true;
                }

                return false;
            }
        }

        private struct Number
        {
            public readonly int lineIndex;
            public readonly int columnIndex;
            public readonly int nbDigits;
            public readonly int value;

            public Number(int pLineIndex, int pColumnIndex, int pNbDigits, int pValue)
            {
                lineIndex = pLineIndex;
                columnIndex = pColumnIndex;
                nbDigits = pNbDigits;
                value = pValue;
            }

            public bool IncludesCoordinates(int pLineIndex, int pColumnIndex)
            {
                return pLineIndex == lineIndex
                    && pColumnIndex >= columnIndex
                    && pColumnIndex < columnIndex + nbDigits;
            }
        }

        private static bool IsCharASymbol(char pSomeChar)
        {
            return !Char.IsDigit(pSomeChar) && pSomeChar != PERIOD;
        }

        private static bool IsCharInEngineSchematicsASymbol(List<char[]> pEngineSchematics, int pLineIndex, int pColumnIndex)
        {
            if(pLineIndex < 0 || pLineIndex >= pEngineSchematics.Count)
            {
                return false;
            }

            char[] schematicsLine = pEngineSchematics[pLineIndex];

            if(pColumnIndex < 0 || pColumnIndex >= schematicsLine.Length)
            {
                return false;
            }

            return IsCharASymbol(schematicsLine[pColumnIndex]);
        }

        private static bool IsNumberAdjacentToASymbol(List<char[]> pEngineSchematics, Number pNumber)
        {
            int nbDigits = pNumber.nbDigits;

            int lineIndex = pNumber.lineIndex;
            int prevLineIndex = lineIndex - 1;
            int nextLineIndex = lineIndex + 1;

            int startColumn = pNumber.columnIndex;
            int prevColumnIndex = startColumn - 1;

            int endColumn = pNumber.columnIndex + nbDigits - 1;
            int nextColumn = endColumn + 1;

            if (
                IsCharInEngineSchematicsASymbol(pEngineSchematics, prevLineIndex, prevColumnIndex)
                || IsCharInEngineSchematicsASymbol(pEngineSchematics, lineIndex, prevColumnIndex)
                || IsCharInEngineSchematicsASymbol(pEngineSchematics, nextLineIndex, prevColumnIndex))
            {
                return true;
            }

            for (int j = startColumn; j <= endColumn; j++)
            {
                if (
                    IsCharInEngineSchematicsASymbol(pEngineSchematics, prevLineIndex, j)
                    || IsCharInEngineSchematicsASymbol(pEngineSchematics, nextLineIndex, j))
                {
                    return true;
                }
            }

            if (
                IsCharInEngineSchematicsASymbol(pEngineSchematics, prevLineIndex, nextColumn)
                || IsCharInEngineSchematicsASymbol(pEngineSchematics, lineIndex, nextColumn)
                || IsCharInEngineSchematicsASymbol(pEngineSchematics, nextLineIndex, nextColumn))
            {
                return true;
            }

            return false;
        }

        private static void Main(string[] args)
        {
            string inputPath = args[0];
            List<char[]> engineSchematics = new List<char[]>();

            List<Number> numbers = new List<Number>();
            List<Gear> gears = new List<Gear>();

            using (StreamReader reader = new StreamReader(inputPath))
            {
                int lineIndex = 0;
                string? line = null;
                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim();
                    char[] lineChars = line.ToCharArray();
                    engineSchematics.Add(lineChars);

                    List<Number> numbersOnLine = new List<Number>();
                    List<Gear> gearsOnLine = new List<Gear>();
                    ParseEngineSchematicsLine(line, lineIndex, numbersOnLine, gearsOnLine);
                    numbers.AddRange(numbersOnLine);
                    gears.AddRange(gearsOnLine);

                    lineIndex++;
                }
            }

            int partNumberSum = 0;
            foreach (Number number in numbers)
            {
                if (IsNumberAdjacentToASymbol(engineSchematics, number))
                {
                    partNumberSum += number.value;
                }

                foreach (Gear gear in gears)
                {
                    gear.CheckIfNumberIsAdjacent(number);
                }
            }

            int gearRatioSum = 0;
            foreach (Gear gear in gears)
            {
                if (gear.NbAdjacentNumbers == 2)
                {
                    gearRatioSum += gear.CalculateGearRatio();
                }
            }

            Console.WriteLine("Puzzle 1: " + partNumberSum);
            Console.WriteLine("Puzzle 2: " + gearRatioSum);
        }

        private static Number ParseNumberFromEngineSchematicsLine(
            string pEngineSchematicLine, int pLineIndex, int pColumnIndex)
        {
            List<char> digits = new List<char>();

            int lineLength = pEngineSchematicLine.Length;
            for (int j = pColumnIndex; j < lineLength; j++)
            {
                char schematicsChar = pEngineSchematicLine[j];

                if (!Char.IsDigit(schematicsChar))
                {
                    break;
                }

                digits.Add(schematicsChar);
            }

            int nbDigits = digits.Count;

            if (nbDigits > 0)
            {
                int value = int.Parse(new String(digits.ToArray()));
                return new Number(pLineIndex, pColumnIndex, nbDigits, value);
            }

            return new Number(-1, -1, -1, 0);
        }

        private static void ParseEngineSchematicsLine(
            string pEngineSchematicLine, int pLineIndex, List<Number> pNumbers, List<Gear> pGears)
        {
            pNumbers.Clear();
            pGears.Clear();

            int lineLength = pEngineSchematicLine.Length;
            for (int j = 0; j < lineLength; j++)
            {
                char schematicChar = pEngineSchematicLine[j];

                if (Char.IsDigit(schematicChar))
                {
                    Number number = ParseNumberFromEngineSchematicsLine(pEngineSchematicLine, pLineIndex, j);
                    pNumbers.Add(number);
                    j += number.nbDigits - 1;
                }
                else if (schematicChar == ASTERISK)
                {
                    pGears.Add(new Gear(pLineIndex, j));
                }
            }
        }
    }
}
