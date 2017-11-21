namespace Trivia
{
    internal class Player
    {
        public Player(string name)
        {
            Name = name;
        }

        public string Name { get; }
        public int Location { get; private set; } = 0;

        public void Move(int roll)
        {
            Location = (Location + roll) % 12;
        }
    }
}