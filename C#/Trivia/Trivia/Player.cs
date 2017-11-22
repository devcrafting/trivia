using System;

namespace Trivia
{
    internal class Player
    {
        private int _goldCoins = 0;

        public Player(string name)
        {
            Name = name;
        }

        public string Name { get; }
        public int Location { get; private set; } = 0;
        public bool IsWinner => _goldCoins == 6;

        public void Move(int roll)
        {
            Location = (Location + roll) % 12;
            Console.WriteLine($"{Name}'s new location is {Location}");
        }

        public void WinAGoldCoin()
        {
            _goldCoins++;
            Console.WriteLine($"{Name} now has {_goldCoins} Gold Coins.");
        }
    }
}