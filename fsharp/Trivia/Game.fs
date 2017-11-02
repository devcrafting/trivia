module TriviaGame

type Player = 
    {
        Name: string
        Place: int
        GoldCoins: int
        IsInPenaltyBox: bool
    }
    static member WithName name = 
        { Name = name; Place = 0; GoldCoins = 0; IsInPenaltyBox = false }

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

type GameState =
    | WaitingPlayers
    | Rolling of PlayerTurn
and PlayerTurn = {
    Player: Player
    NextPlayers: Player list
}

let addPlayer name gameState =
    let player = Player.WithName name
    let playerTurn =
        match gameState with
        | WaitingPlayers -> { Player = player; NextPlayers = [] }
        | Rolling p -> { p with NextPlayers = p.NextPlayers @ [ player ]}
    
    printfn "%s was added" name
    printfn "They are player number %i" (playerTurn.NextPlayers.Length + 1)
    Rolling playerTurn