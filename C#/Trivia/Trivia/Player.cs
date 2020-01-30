namespace Trivia
{
    internal class Player
    {
        public Player(string name)
        {
            Name = name;
            IsInPenaltyBox = false;
        }

        public string Name { get; }
        public bool IsInPenaltyBox { get; private set; }

        public void GoToPenaltyBox()
        {
            IsInPenaltyBox = true;
        }
    }
}