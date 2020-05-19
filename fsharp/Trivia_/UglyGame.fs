
module UglyTrivia

open System;
open System.Collections.Generic;

type Category =
    | Science
    | Rock
    | Sports
    | Pop

type GameState =
    {
        Players: List<string>
        Places: int array
        Purses: int array
        InPenaltyBox: bool array
        Questions: Map<Category, string list>
        PopQuestions: string list
        ScienceQuestions: string list
        SportsQuestions: string list
        RockQuestions: string list
        CurrentPlayer: int
        GettingOutOfPenaltyBox: bool
    }

let newGame () =
    let popQuestions = [1..50] |> List.map (fun i -> "Pop Question " + i.ToString())
    let scienceQuestions = [1..50] |> List.map (fun i -> "Science Question " + i.ToString())
    let sportsQuestions = [1..50] |> List.map (fun i -> "Sports Question " + i.ToString())
    let rockQuestions = [1..50] |> List.map (fun i -> "Rock Question " + i.ToString())
    {
        Players = List<string>()
        Places = Array.create 6 0
        Purses = Array.create 6 0
        InPenaltyBox = Array.create 6 false
        Questions = Map.ofList [(Sports, sportsQuestions); (Science, scienceQuestions); (Rock, rockQuestions); (Pop, popQuestions)]
        PopQuestions = popQuestions
        ScienceQuestions = scienceQuestions
        SportsQuestions = sportsQuestions
        RockQuestions = rockQuestions
        CurrentPlayer = 0
        GettingOutOfPenaltyBox = false
    }
    
let askAndDiscardQuestion category state =
    let first::tail = state.Questions.[category]
    Console.WriteLine(first)
    { state with Questions = state.Questions |> Map.add category tail }
    
type UglyGame() as this =

    let mutable state = newGame()
        
    let players = List<string>()

    let places = Array.create 6 0
    let purses = Array.create 6 0

    let inPenaltyBox = Array.create 6 false

    let mutable currentPlayer = 0;
    let mutable isGettingOutOfPenaltyBox = false;

    member this.isPlayable(): bool =
        this.howManyPlayers() >= 2

    member this.add(playerName: String): bool =
        players.Add(playerName);
        places.[this.howManyPlayers()] <- 0;
        purses.[this.howManyPlayers()] <- 0;
        inPenaltyBox.[this.howManyPlayers()] <- false;

        Console.WriteLine(playerName + " was added");
        Console.WriteLine("They are player number " + players.Count.ToString());
        true

    member this.howManyPlayers(): int =
        players.Count;

    member this.roll(roll: int) =
        Console.WriteLine(players.[currentPlayer] + " is the current player");
        Console.WriteLine("They have rolled a " + roll.ToString());

        if inPenaltyBox.[currentPlayer] then
            if roll % 2 <> 0 then
                isGettingOutOfPenaltyBox <- true;

                Console.WriteLine(players.[currentPlayer].ToString() + " is getting out of the penalty box");
                places.[currentPlayer] <- places.[currentPlayer] + roll;
                if places.[currentPlayer] > 11 then places.[currentPlayer] <- places.[currentPlayer] - 12;

                Console.WriteLine(players.[currentPlayer]
                                    + "'s new location is "
                                    + places.[currentPlayer].ToString());
                Console.WriteLine("The category is " + this.currentCategory().ToString());
                this.askQuestion();
               
            else
                Console.WriteLine(players.[currentPlayer].ToString() + " is not getting out of the penalty box");
                isGettingOutOfPenaltyBox <- false;

        else
            places.[currentPlayer] <- places.[currentPlayer] + roll;
            if places.[currentPlayer] > 11 then places.[currentPlayer] <- places.[currentPlayer] - 12;

            Console.WriteLine(players.[currentPlayer]
                                + "'s new location is "
                                + places.[currentPlayer].ToString());
            Console.WriteLine("The category is " + this.currentCategory().ToString());
            this.askQuestion();

        
    member private this.askQuestion() =
        state <- askAndDiscardQuestion (this.currentCategory()) state

    member private this.currentCategory() =
        if (places.[currentPlayer] % 4 = 0) then Pop
        elif (places.[currentPlayer] % 4 = 1) then Science
        elif (places.[currentPlayer] % 4 = 2) then Sports
        else Rock

    member this.wasCorrectlyAnswered(): bool =
        if inPenaltyBox.[currentPlayer] then
            if isGettingOutOfPenaltyBox then
                Console.WriteLine("Answer was correct!!!!");
                purses.[currentPlayer] <- purses.[currentPlayer] + 1;
                Console.WriteLine(players.[currentPlayer]
                                    + " now has "
                                    + purses.[currentPlayer].ToString()
                                    + " Gold Coins.");

                let winner = this.didPlayerWin();
                currentPlayer <- currentPlayer + 1;
                if currentPlayer = players.Count then currentPlayer <- 0;
                
                winner;
            else
                currentPlayer <- currentPlayer + 1;
                if currentPlayer = players.Count then currentPlayer <- 0;
                true;
        else

            Console.WriteLine("Answer was corrent!!!!");
            purses.[currentPlayer] <- purses.[currentPlayer] + 1;
            Console.WriteLine(players.[currentPlayer]
                                + " now has "
                                + purses.[currentPlayer].ToString()
                                + " Gold Coins.");

            let winner = this.didPlayerWin();
            currentPlayer <- currentPlayer + 1;
            if (currentPlayer = players.Count) then currentPlayer <- 0;

            winner;

    member this.wrongAnswer(): bool=
        Console.WriteLine("Question was incorrectly answered");
        Console.WriteLine(players.[currentPlayer] + " was sent to the penalty box");
        inPenaltyBox.[currentPlayer] <- true;

        currentPlayer <- currentPlayer + 1;
        if (currentPlayer = players.Count) then currentPlayer <- 0;
        true;


    member private this.didPlayerWin(): bool =
        not (purses.[currentPlayer] = 6);


module GameRunner = 
    [<EntryPoint>]
    let main argv = 
        let mutable isFirstRound = true;
        let mutable notAWinner = false;
        let aGame = UglyGame();

        aGame.add("Chet") |> ignore;
        aGame.add("Pat") |> ignore;
        aGame.add("Sue") |> ignore;

        let rand = new Random(0);

        while isFirstRound || notAWinner do
            isFirstRound <- false; 
            aGame.roll(rand.Next(5) + 1);

            if (rand.Next(9) = 7) then
                notAWinner <- aGame.wrongAnswer();
            else
                notAWinner <- aGame.wasCorrectlyAnswered();

        0
