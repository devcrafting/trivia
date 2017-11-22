namespace Trivia
{
    public class PlayerRolledDice
    {
        public PlayerRolledDice(string playerName, int roll)
        {
            PlayerName = playerName;
            Roll = roll;
        }

        public string PlayerName { get; }
        public int Roll { get; }
    }
}