namespace AoC2023
{
    internal struct RaceRecord
    {
        public readonly int allowedTime;
        public readonly int distance;

        public RaceRecord(int pAllowedTime, int pDistance)
        {
            allowedTime = pAllowedTime;
            distance = pDistance;
        }
    }
}
