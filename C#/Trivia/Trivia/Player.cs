using System;

namespace Trivia
{
    internal class Player
    {
        private int _goldCoins;
        public string Name { get; }

        public int Location { get; private set; }
        public bool IsInPenaltyBox { get; private set; }

        public Player(string name)
        {
            Name = name;
        }

        public void Move(int roll) => Location = (Location + roll) % 12;

        public void WinAGoldCoin()
        {
            _goldCoins++;
            Console.WriteLine(Name
                              + " now has "
                              + _goldCoins
                              + " Gold Coins.");
        }

        public bool DidPlayerWin() => _goldCoins == 6;

        public void SendToPenaltyBox()
        {
            IsInPenaltyBox = true;
        }
    }
}