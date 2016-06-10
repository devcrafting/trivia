namespace UglyTrivia
{
    internal class Player
    {
        public Player(string playerName)
        {
            Name = playerName;
            Purses = 0;
            Place = 0;
            IsInPenaltyBox = false;
        }

        public int Purses { get; private set; }

        public string Name { get; private set; }

        public int Place { get; private set; }

        public bool IsInPenaltyBox { get; private set; }

        public void WinOnePurse()
        {
            Purses++;
        }

        public void Move(int roll)
        {
            Place = (Place + roll) % 12;
        }

        public void GoToPenaltyBox()
        {
            IsInPenaltyBox = true;
        }
    }
}