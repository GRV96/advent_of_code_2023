using System.Text.RegularExpressions;

namespace AoC2023
{
    internal class Day8
    {
        private const string START_NODE = "AAA";
        private const string DESTINATION_NODE = "ZZZ";

        private static DesertNode MakeNodeFromRegexMatch(Match pRegexMatch)
        {
            string nodeName = pRegexMatch.Groups["node"].Value;
            string leftNode = pRegexMatch.Groups["leftNode"].Value;
            string rightNode = pRegexMatch.Groups["rightNode"].Value;
            return new DesertNode(nodeName, leftNode, rightNode);
        }

        static void Main(string[] args)
        {
            string inputPath = args[0];

            Regex instructionPattern = new Regex(@"[LR]+");
            Regex nodePattern = new Regex(@"(?<node>[A-Z]{3}) = \((?<leftNode>[A-Z]{3}), (?<rightNode>[A-Z]{3})\)");

            NavigationInstructions navigationInstructions = null;
            Dictionary<string, DesertNode> desertNodes = new Dictionary<string, DesertNode>();

            using (StreamReader reader = new StreamReader(inputPath))
            {
                string? line = null;
                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim();

                    if (navigationInstructions == null)
                    {
                        Match instructionMatch = instructionPattern.Match(line);
                        if (instructionMatch.Success)
                        {
                            navigationInstructions = new NavigationInstructions(line);
                        }
                    }
                    else
                    {
                        Match nodeMatch = nodePattern.Match(line);
                        if (nodeMatch.Success)
                        {
                            DesertNode desertNode = MakeNodeFromRegexMatch(nodeMatch);
                            desertNodes[desertNode.name] = desertNode;
                        }
                    }
                }
            }

            DesertNode node = desertNodes[START_NODE];
            int nbSteps = 0;
            while (node.name != DESTINATION_NODE)
            {
                char instruction = navigationInstructions.GetNextInstruction();
                string nextNodeName = node.GetNodeNameOnSide((Direction)instruction);
                node = desertNodes[nextNodeName];
                nbSteps++;
            }

            Console.WriteLine("Puzzle 1: " + nbSteps);
        }
    }
}
