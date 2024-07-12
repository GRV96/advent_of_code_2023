using System;

namespace AoC2023
{
    struct CubeSet
    {
        public readonly int nbRedCubes;
        public readonly int nbGreenCubes;
        public readonly int nbBlueCubes;

        public CubeSet(int pNbRedCubes, int pNbGreenCubes, int pnbBlueCubes)
        {
            nbRedCubes = pNbRedCubes;
            nbGreenCubes = pNbGreenCubes;
            nbBlueCubes = pnbBlueCubes;
        }

        public bool IsPossible(int pNbRedCubes, int pNbGreenCubes, int pNbBlueCubes)
        {
            return nbRedCubes <= pNbRedCubes && nbGreenCubes <= pNbGreenCubes && nbBlueCubes <= pNbBlueCubes;
        }
    }

    struct CubeGame
    {
        public readonly int gameId;
        public readonly CubeSet[] cubeSets;

        public CubeGame(int pGameId, CubeSet[] pCubeSets)
        {
            gameId = pGameId;

            int nbCubeSets = pCubeSets.Length;
            cubeSets = new CubeSet[nbCubeSets];
            Array.Copy(pCubeSets, cubeSets, nbCubeSets);
        }

        public void GetRequiredNbCubesPerColor(out int pReqNbRedCubes, out int pReqNbGreenCubes, out int pReqNbBlueCubes)
        {
            int maxNbRedCubes = -1;
            int maxNbGreenCubes = -1;
            int maxNbBlueCubes = -1;

            foreach (CubeSet cubeSet in cubeSets)
            {
                int nbRedCubes = cubeSet.nbRedCubes;
                if (nbRedCubes > maxNbRedCubes)
                {
                    maxNbRedCubes = nbRedCubes;
                }

                int nbGreenCubes = cubeSet.nbGreenCubes;
                if (nbGreenCubes > maxNbGreenCubes)
                {
                    maxNbGreenCubes = nbGreenCubes;
                }

                int nbBlueCubes = cubeSet.nbBlueCubes;
                if (nbBlueCubes > maxNbBlueCubes)
                {
                    maxNbBlueCubes = nbBlueCubes;
                }
            }

            pReqNbRedCubes = maxNbRedCubes;
            pReqNbGreenCubes = maxNbGreenCubes;
            pReqNbBlueCubes = maxNbBlueCubes;
        }

        public bool IsPossible(int pNbRedCubes, int pNbGreenCubes, int pNbBlueCubes)
        {
            foreach (CubeSet cubeSet in cubeSets)
            {
                if (!cubeSet.IsPossible(pNbRedCubes, pNbGreenCubes, pNbBlueCubes))
                {
                    return false;
                }
            }

            return true;
        }
    }

    class Day2
    {
        private const string RED = "red";
        private const string GREEN = "green";
        private const string BLUE = "blue";

        private const char COLON = ':';
        private const string COMMA_SPACE = ", ";
        private const string SEMICOLON_SPACE = "; ";
        private const char SPACE = ' ';

        private static CubeSet ParseCubeSetData(string pCubeSetData)
        {
            string[] allCubesData = pCubeSetData.Split(COMMA_SPACE);
            int nbRedCubes = 0;
            int nbGreenCubes = 0;
            int nbBlueCubes = 0;

            foreach (string colorData in allCubesData)
            {
                string[] splitData = colorData.Split(SPACE);
                int nbCubes = int.Parse(splitData[0]);
                string cubeColor = splitData[1];

                switch (cubeColor)
                {
                    case RED:
                        nbRedCubes = nbCubes;
                        break;
                    case GREEN:
                        nbGreenCubes = nbCubes;
                        break;
                    case BLUE:
                        nbBlueCubes = nbCubes;
                        break;
                }
            }

            return new CubeSet(nbRedCubes, nbGreenCubes, nbBlueCubes);
        }

        private static CubeGame ParsePuzzleLine(string pPuzzleLine)
        {
            int colonIndex = pPuzzleLine.IndexOf(COLON);
            int cubeGameId = int.Parse(pPuzzleLine.Substring(5, colonIndex-5));

            int dataStartIndex = colonIndex + 2;
            string rawCubeSetData = pPuzzleLine.Substring(dataStartIndex);
            string[] allSetsData = rawCubeSetData.Split(SEMICOLON_SPACE);

            int nbCubeSets = allSetsData.Length;
            CubeSet[] cubeSets = new CubeSet[nbCubeSets];
            for (int i=0; i<nbCubeSets; i++)
            {
                string setData = allSetsData[i];
                cubeSets[i] = ParseCubeSetData(setData);
            }

            return new CubeGame(cubeGameId, cubeSets);
        }

        static void Main(string[] args)
        {
            string inputPath = args[0];
            int possibleGameIdSum = 0;
            int reqGamePowerSum = 0;

            using (StreamReader reader = new StreamReader(inputPath))
            {
                string? line = null;

                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim();
                    CubeGame cubeGame = ParsePuzzleLine(line);

                    if(cubeGame.IsPossible(12, 13, 14))
                    {
                        possibleGameIdSum += cubeGame.gameId;
                    }

                    int reqNbRedCubes;
                    int reqNbGreenCubes;
                    int reqNbBlueCubes;
                    cubeGame.GetRequiredNbCubesPerColor(out reqNbRedCubes, out reqNbGreenCubes, out reqNbBlueCubes);
                    reqGamePowerSum += reqNbRedCubes * reqNbGreenCubes * reqNbBlueCubes;
                }
            }

            Console.WriteLine("Puzzle 1: " + possibleGameIdSum);
            Console.WriteLine("Puzzle 2: " + reqGamePowerSum);
        }
    }
}