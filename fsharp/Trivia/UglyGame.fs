
namespace UglyTrivia

open System;
open System.Collections.Generic;
open System.Linq;
open System.Text;

open TriviaGame

type Game() =

    let players = List<Player>()
    let mutable questionsStacks = 
        ["Pop"; "Science"; "Sports"; "Rock" ]
        |> List.map generateQuestionsStack

    let mutable currentPlayer = 0;
    let mutable isGettingOutOfPenaltyBox = false;

    member this.isPlayable(): bool =
        this.howManyPlayers() >= 2

    member this.add(playerName: String): bool =
        players.Add({ Name = playerName; Place = 0; GoldCoins = 0; IsInPenaltyBox = false });

        true

    member this.howManyPlayers(): int =
        players.Count;

    member this.roll player (roll: int) =
        let player = TriviaGame.roll player roll

        if players.[currentPlayer].IsInPenaltyBox then
            if roll % 2 <> 0 then
                isGettingOutOfPenaltyBox <- true;

                Console.WriteLine(players.[currentPlayer].Name + " is getting out of the penalty box");
                players.[currentPlayer] <- move players.[currentPlayer] roll;
                this.askQuestion();
               
            else
                Console.WriteLine(players.[currentPlayer].Name + " is not getting out of the penalty box");
                isGettingOutOfPenaltyBox <- false;

        else
            players.[currentPlayer] <- move players.[currentPlayer] roll;
            this.askQuestion();
        player

    member private this.askQuestion() =
        let questionsStack = questionsStacks.[this.currentCategory()]
        Console.WriteLine("The category is " + questionsStack.Name)
        Console.WriteLine(questionsStack.Questions |> List.head)
        questionsStacks <- 
            questionsStacks
            |> List.map (fun x -> if x = questionsStack then { questionsStack with Questions = List.tail questionsStack.Questions } else x)

    member private this.currentCategory() =
        players.[currentPlayer].Place % 4
        
    member this.wasCorrectlyAnswered nextPlayers player =
        if players.[currentPlayer].IsInPenaltyBox then
            if isGettingOutOfPenaltyBox then
                Console.WriteLine("Answer was correct!!!!");
                players.[currentPlayer] <- winAGoldCoin players.[currentPlayer];
            else ()
        else
            Console.WriteLine("Answer was corrent!!!!");
            players.[currentPlayer] <- winAGoldCoin players.[currentPlayer];

        let player = players.[currentPlayer]

        currentPlayer <- currentPlayer + 1;
        if (currentPlayer = players.Count) then currentPlayer <- 0;
        nextPlayer player nextPlayers

    member this.wrongAnswer nextPlayers player =
        let playerInPenaltyBox = goToPenaltyBox players.[currentPlayer]
        players.[currentPlayer] <- playerInPenaltyBox;

        currentPlayer <- currentPlayer + 1;
        if (currentPlayer = players.Count) then currentPlayer <- 0;
        nextPlayer playerInPenaltyBox nextPlayers;

module GameRunner = 
    [<EntryPoint>]
    let main argv = 
        let aGame = Game();

        aGame.add("Chet") |> ignore;
        aGame.add("Pat") |> ignore;
        aGame.add("Sue") |> ignore;

        let rand = 
            match Array.toList argv with
            | seed::tail -> new Random(int seed)
            | _ -> new Random()

        let gameState =
            WaitingPlayers
            |> addPlayer "Chet"
            |> addPlayer "Pat"
            |> addPlayer "Sue" 

        let randomizeAnswer (rand: Random) nextPlayers =
            if (rand.Next(9) = 7) then
                aGame.wrongAnswer nextPlayers
            else
                aGame.wasCorrectlyAnswered nextPlayers

        let rec nextPlayerPlay = function 
            | Rolling p as gameState -> 
                aGame.roll p.Player (rand.Next(5) + 1)
                |> randomizeAnswer rand p.NextPlayers
                |> nextPlayerPlay
            | Won -> ()

        nextPlayerPlay gameState

        0
