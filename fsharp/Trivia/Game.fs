module TriviaGame

type Player = 
    {
        Name: string
        Location: int
        GoldCoins: int
        IsInPenaltyBox: bool
    }
    static member WithName name =
        { Name = name; Location = 0; GoldCoins = 0; IsInPenaltyBox = false }

let move diceValue player =
    let newLocation = (player.Location + diceValue) % 12
    printfn "%s's new location is %i" player.Name newLocation
    { player with Location = newLocation }

let winAGoldCoin player =
    let goldCoinsWon = player.GoldCoins + 1;
    printfn "%s now has %i Gold Coins." player.Name goldCoinsWon
    { player with GoldCoins = goldCoinsWon }

let goToPenaltyBox player =
    printfn "Question was incorrectly answered"
    printfn "%s was sent to the penalty box" player.Name
    { player with IsInPenaltyBox = true }

type QuestionsStack = {
    Category: string
    Questions: Question list
}
and Question = string

let generateQuestionsStack category =
    let questions = [1..50] |> List.map (fun i -> sprintf "%s Question %i" category i)
    { Category = category; Questions = questions}

let askAndDiscardQuestion location (questionsStacks: QuestionsStack list) =
    let questionsStack = questionsStacks.[location % 4]
    printfn "The category is %s" questionsStack.Category
    let firstQuestion = questionsStack.Questions |> List.head
    printfn "%s" firstQuestion
    questionsStacks
    |> List.map (fun x -> 
        if x = questionsStack then { questionsStack with Questions = questionsStack.Questions |> List.tail }
        else x)

type GameState =
    | GameOutOfTheBox of QuestionsStack list
    | Playing of GameTurn
    | Won
and GameTurn = {
    Player: Player
    GettingOutOfPenaltyBox: bool
    NextPlayers: Player list
    QuestionsStacks: QuestionsStack list
}

let invitePlayer name gameState =
    let player = Player.WithName name
    let firstGameTurn =
        match gameState with
        | GameOutOfTheBox q -> { Player = player; NextPlayers = []; QuestionsStacks = q; GettingOutOfPenaltyBox = false }
        | Playing turn -> { turn with NextPlayers = turn.NextPlayers @ [ player ] }
    printfn "%s was added" name
    printfn "They are player number %i" (firstGameTurn.NextPlayers.Length + 1)
    Playing firstGameTurn

let prepareNextTurn currentPlayer currentTurn =
    if currentPlayer.GoldCoins = 6 then Won
    else
        Playing { currentTurn with
                    Player = currentTurn.NextPlayers |> List.head
                    NextPlayers = (currentTurn.NextPlayers |> List.tail) @ [ currentPlayer ] }

let private moveAskAndDiscardQuestion diceValue turn =
    let player = turn.Player |> move diceValue
    let questionsStacks = turn.QuestionsStacks |> askAndDiscardQuestion player.Location
    { turn with 
            Player = player
            GettingOutOfPenaltyBox = true
            QuestionsStacks = questionsStacks }

let roll diceValue turn =
    printfn "%s is the current player" turn.Player.Name
    printfn "They have rolled a %i" diceValue
    match turn.Player with
    | p when p.IsInPenaltyBox && diceValue % 2 = 0 ->
        printfn "%s is not getting out of the penalty box" p.Name
        { turn with GettingOutOfPenaltyBox = false }
    | p when p.IsInPenaltyBox ->
        printfn "%s is getting out of the penalty box" p.Name
        turn |> moveAskAndDiscardQuestion diceValue
    | p ->
        turn |> moveAskAndDiscardQuestion diceValue

let answerCorrectly turn =
    let player = 
        match turn.Player with
        | p when not p.IsInPenaltyBox ->
            printfn "Answer was corrent!!!!"
            p |> winAGoldCoin
        | p when p.IsInPenaltyBox && turn.GettingOutOfPenaltyBox ->
            printfn "Answer was correct!!!!"
            p |> winAGoldCoin
        | p -> p 
    prepareNextTurn player turn

let answerBadly turn =
    let player = turn.Player |> goToPenaltyBox
    prepareNextTurn player turn
