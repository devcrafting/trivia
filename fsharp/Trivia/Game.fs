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
    printfn "Question was incorrectly answered"
    printfn "%s was sent to the penalty box" player.Name
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
    | Won
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

let nextPlayer currentPlayer = function
    | _ when currentPlayer.GoldCoins = 6 -> Won
    | Rolling p -> 
        Rolling { p with 
                    Player = p.NextPlayers |> List.head
                    NextPlayers = (p.NextPlayers |> List.tail) @ [ currentPlayer ] }
    | x -> x

let roll gameState dice =
    match gameState with 
    | Rolling p ->
        printfn "%s is the current player" p.Player.Name
        printfn "They have rolled a %i" dice
        gameState