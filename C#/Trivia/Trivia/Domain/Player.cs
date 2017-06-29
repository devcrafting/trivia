using System;

namespace Trivia
{
    public class Player
    {
        private IDispatchEvent _dispatchEvent;
        public string Name { get; private set; }

        public int Place { get; private set; }

        public int GoldCoins { get; private set; }

        public bool InPenaltyBox { get; set; }

        public Player(string name, IDispatchEvent dispatchEvent)
        {
            Name = name;
            _dispatchEvent = dispatchEvent;
            Place = 0;
            GoldCoins = 0;
            InPenaltyBox = false;
        }


        public void Move(int roll)
        {
            Place += roll;
            if (Place > 11) Place -= 12;
        }

        public void WinAGoldCoin()
        {
            GoldCoins++;
            _dispatchEvent.Dispatch(new PlayerWonAGoldPoint(Name, GoldCoins));
        }

        public void GoToPenaltyBox()
        {
            InPenaltyBox = true;
        }

        public bool IsWinner()
        {
            return GoldCoins == 6;
        }
    }
}