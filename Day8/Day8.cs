using System.Text.RegularExpressions;

namespace AoC2023
{
    internal class Day8
    {
        private const char START_LETTER = 'A';
        private const char DESTINATION_LETTER = 'Z';
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
            Regex nodePattern = new Regex(@"(?<node>\w{3}) = \((?<leftNode>\w{3}), (?<rightNode>\w{3})\)");

            NavigationInstructions navigationInstructions = null;
            Dictionary<string, DesertNode> desertNodes = new Dictionary<string, DesertNode>();
            List<DesertNode> nodesPuzzle2 = new List<DesertNode>();

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

                    Match nodeMatch = nodePattern.Match(line);
                    if (nodeMatch.Success)
                    {
                        DesertNode desertNode = MakeNodeFromRegexMatch(nodeMatch);
                        string desertNodeName = desertNode.name;
                        desertNodes[desertNodeName] = desertNode;

                        if(desertNodeName[desertNodeName.Length - 1] == START_LETTER)
                        {
                            nodesPuzzle2.Add(desertNode);
                        }
                    }
                }
            }

            int nbNodesPuzzle2 = nodesPuzzle2.Count;
            int nbStepsPuzzle1 = 0;
            int nbStepsPuzzle2 = 0;
            bool isPuzzle1Done = !desertNodes.TryGetValue(START_NODE, out DesertNode nodePuzzle1);
            bool isPuzzle2Done = false;
            while (!isPuzzle1Done || !isPuzzle2Done)
            {
                char instruction = navigationInstructions.GetNextInstruction();
                Direction direction = (Direction)instruction;

                if (!isPuzzle1Done)
                {
                    string nextNodeName = nodePuzzle1.GetNodeNameOnSide(direction);
                    nodePuzzle1 = desertNodes[nextNodeName];
                    nbStepsPuzzle1++;
                    isPuzzle1Done = nextNodeName == DESTINATION_NODE;
                }

                if (!isPuzzle2Done)
                {
                    isPuzzle2Done = true;

                    for (int i = 0; i < nbNodesPuzzle2; i++)
                    {
                        DesertNode nodePuzzle2 = nodesPuzzle2[i];
                        string nextNodeName = nodePuzzle2.GetNodeNameOnSide(direction);
                        nodePuzzle2 = desertNodes[nextNodeName];
                        nodesPuzzle2[i] = nodePuzzle2;

                        if (nextNodeName[nextNodeName.Length - 1] != DESTINATION_LETTER)
                        {
                            isPuzzle2Done = false;
                        }
                    }

                    nbStepsPuzzle2++;
                }
            }

            Console.WriteLine("Puzzle 1: " + nbStepsPuzzle1);
            Console.WriteLine("Puzzle 2: " + nbStepsPuzzle2);
        }
    }
}
