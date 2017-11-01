module TriviaGame

type Player = {
    Name: string
    Place: int
    GoldCoins: int
    IsInPenaltyBox: bool
}

let move player roll =
    { player with Place = (player.Place + roll) % 12 }

let winAGoldCoin player =
    { player with GoldCoins = player.GoldCoins + 1  }

let goToPenaltyBox player = 
    { player with IsInPenaltyBox = true }