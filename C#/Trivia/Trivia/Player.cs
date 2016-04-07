namespace UglyTrivia
{
    internal class Player
    {
        public Player(string playerName)
        {
            Name = playerName;
        }

        public string Name { get; private set; }
        public string Purses { get; private set; }
        public int[] Places { get; private set; }
        public string inPenaltyBox { get; private set; }
    }
}