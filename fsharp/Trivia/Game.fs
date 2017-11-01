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

type QuestionsStack = {
    Name: string
    Questions: Question list
}
and Question = string

let generateQuestionsStack category =
    let questions = [1..50] |> List.map (fun i -> sprintf "%s Question %i" category i)
    { Name = category; Questions = questions }
