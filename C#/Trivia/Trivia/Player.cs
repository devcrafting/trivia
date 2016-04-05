namespace UglyTrivia
{
    internal class Player
    {
        public Player(string playerName)
        {
            Name = playerName;
            Purses = 0;
        }

        public int Purses { get; private set; }

        public string Name { get; private set; }

        public void WinOnePurse()
        {
            Purses++;
        }
    }
}