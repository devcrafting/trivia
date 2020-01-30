using System;
using System.Collections.Generic;

namespace Trivia
{
    internal class Player
    {
        public Player(string name)
        {
            Name = name;
            IsInPenaltyBox = false;
            Place = 0;
        }

        public string Name { get; }
        public bool IsInPenaltyBox { get; private set; }
        public int Place { get; private set; }
        public int GoldCoins { get; private set; }

        public void GoToPenaltyBox()
        {
            IsInPenaltyBox = true;
        }

        public void Move(in int roll)
        {
            Place = (Place + roll) % 12;
            Console.WriteLine($"{Name}'s new location is {Place}");
        }

        public void WinAGoldCoin()
        {
            GoldCoins++;
            Console.WriteLine($"{Name} now has {GoldCoins} Gold Coins.");
        }

        public bool HasNotWon() => GoldCoins != 6;
    }
}