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
    let newPlace = (player.Place + roll) % 12
    printfn "%s's new location is %i" player.Name newPlace
    { player with Place = newPlace }

let winAGoldCoin player =
    let goldCoinsWon = player.GoldCoins + 1
    printfn "%s now has %i Gold Coins." player.Name goldCoinsWon
    { player with GoldCoins = goldCoinsWon }

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

let nextPlayer currentPlayer nextPlayers = 
    if currentPlayer.GoldCoins = 6 then Won
    else 
        Rolling { 
            Player = nextPlayers |> List.head
            NextPlayers = (nextPlayers |> List.tail) @ [ currentPlayer ] }

let roll (player:Player) dice =
    printfn "%s is the current player" player.Name
    printfn "They have rolled a %i" dice
    player
