namespace AoC2023
{
    internal class NavigationInstructions
    {
        private readonly string _instructions;
        private readonly int _nbInstructions;
        private int _index;

        public NavigationInstructions(string pInstructions)
        {
            _instructions = pInstructions;
            _nbInstructions = _instructions.Length;
            _index = 0;
        }

        public char GetNextInstruction()
        {
            if (_index >= _nbInstructions)
            {
                _index = 0;
            }

            return _instructions[_index++];
        }
    }
}
