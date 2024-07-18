namespace AoC2023
{
    internal struct DesertNode
    {
        public readonly string name;
        public readonly string leftNode;
        public readonly string rightNode;

        public DesertNode(string pName, string pLeftNode, string pRightNode)
        {
            name = pName;
            leftNode = pLeftNode;
            rightNode = pRightNode;
        }

        public string GetNodeNameOnSide(Direction pSide)
        {
            string nodeName = null;

            switch(pSide)
            {
                case Direction.Left:
                    nodeName = leftNode;
                    break;
                case Direction.Right:
                    nodeName = rightNode;
                    break;
            }

            return nodeName;
        }
    }
}
