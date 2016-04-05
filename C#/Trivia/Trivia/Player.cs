namespace UglyTrivia
{
    internal class Player
    {
        public Player(string playerName)
        {
            Name = playerName;
            Purses = 0;
            Place = 0;
        }

        public int Purses { get; private set; }

        public string Name { get; private set; }

        public int Place { get; private set; }

        public void WinOnePurse()
        {
            Purses++;
        }

        public void Move(int roll)
        {
            Place = (Place + roll) % 12;
        }
    }
}