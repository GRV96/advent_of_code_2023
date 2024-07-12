using AoC2023;

String inputPath = args[0];

List<String> rawLines = DataReading.GetLinesFromFile(inputPath);

Console.WriteLine("Raw Lines");
foreach(string rawLine in rawLines)
{
    Console.WriteLine(rawLine);
}

List<int> linesNbWords = DataReading.ParseDataFromFile<int>(inputPath, str => str.Split(' ').Length);

Console.WriteLine("\nNumber of Words");
foreach(int lineNbWord in linesNbWords)
{
    Console.WriteLine(lineNbWord);
}
Console.WriteLine();
