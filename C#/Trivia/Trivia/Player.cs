namespace Trivia
{
    internal class Player
    {
        public string Name { get; }

        public int Location { get; private set; }

        public Player(string name)
        {
            Name = name;
        }

        public void Move(int roll)
        {
            this.Location = (this.Location + roll) % 12;
        }
    }
}