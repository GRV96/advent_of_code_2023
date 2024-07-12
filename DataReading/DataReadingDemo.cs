using AoC2023;

string inputPath = args[0];

List<string> rawLines = DataReading.GetLinesFromFile(inputPath);

Console.WriteLine("Raw Lines");
foreach(string rawLine in rawLines)
{
    Console.WriteLine(rawLine);
}

List<int> linesNbWords = DataReading.ParseDataFromFile<int>(inputPath, str => str.Split(' ').Length);

Console.WriteLine("\nNumber of Words");
foreach(int nbWords in linesNbWords)
{
    Console.WriteLine(nbWords);
}
Console.WriteLine();
