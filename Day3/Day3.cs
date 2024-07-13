namespace AoC2023
{
    internal class Day3
    {
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
        }

        private static bool IsCharASymbol(char pSomeChar)
        {
            return !Char.IsDigit(pSomeChar) && pSomeChar != '.';
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

            using (StreamReader reader = new StreamReader(inputPath))
            {
                int lineIndex = 0;
                string? line = null;
                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim();
                    char[] lineChars = line.ToCharArray();
                    engineSchematics.Add(lineChars);
                    List<Number> numbersOnLine = SpotNumbersOnEngineSchematicsLine(line, lineIndex);
                    numbers.AddRange(numbersOnLine);
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
            }

            Console.WriteLine("Puzzle 1: " + partNumberSum);
        }

        private static Number ParseNumberFromEngineSchematicsLine(string pEngineSchematicLine, int pLineIndex, int pColumnIndex)
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

        private static List<Number> SpotNumbersOnEngineSchematicsLine(string pEngineSchematicLine, int pLineIndex)
        {
            List<Number> numbers = new List<Number>();

            int lineLength = pEngineSchematicLine.Length;
            for (int j = 0; j < lineLength; j++)
            {
                char schematicChar = pEngineSchematicLine[j];

                if (Char.IsDigit(schematicChar))
                {
                    Number number = ParseNumberFromEngineSchematicsLine(pEngineSchematicLine, pLineIndex, j);
                    numbers.Add(number);
                    j += number.nbDigits - 1;
                }
            }

            return numbers;
        }
    }
}
