using AoC2023;

static int GetNumberFromString(string pSomeString)
{
    char firstDigit = '0';
    foreach(char ch in pSomeString)
    {
        if (Char.IsDigit(ch))
        {
            firstDigit = ch;
            break;
        }
    }

    char lastDigit = '0';
    for (int i = pSomeString.Length - 1; i >= 0; i--)
    {
        char ch = pSomeString[i];

        if (Char.IsDigit(ch))
        {
            lastDigit = ch;
            break;
        }
    }

    int number = int.Parse(new String([firstDigit, lastDigit]));

    return number;
}

List<int> numbers = DataReading.ParseDataFromFile<int>(args[0], GetNumberFromString);

int sum = 0;
foreach (int number in numbers)
{
    sum += number;
}

Console.WriteLine("Puzzle 1: " + sum);
