﻿namespace AoC2023
{
    internal class Day8
    {
        static void Main(string[] args)
        {
            string inputPath = args[0];

            using (StreamReader reader = new StreamReader(inputPath))
            {
                string? line = null;
                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim();
                }
            }

            Console.WriteLine("Hello, World!");
        }
    }
}
