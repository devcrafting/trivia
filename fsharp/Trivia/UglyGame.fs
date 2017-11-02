
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

    member this.roll gameState (roll: int) =
        let gameState = TriviaGame.roll gameState roll

        if players.[currentPlayer].IsInPenaltyBox then
            if roll % 2 <> 0 then
                isGettingOutOfPenaltyBox <- true;

                Console.WriteLine(players.[currentPlayer].Name + " is getting out of the penalty box");
                players.[currentPlayer] <- move players.[currentPlayer] roll;

                Console.WriteLine(players.[currentPlayer].Name
                                    + "'s new location is "
                                    + players.[currentPlayer].Place.ToString());
                this.askQuestion();
               
            else
                Console.WriteLine(players.[currentPlayer].Name + " is not getting out of the penalty box");
                isGettingOutOfPenaltyBox <- false;

        else
            players.[currentPlayer] <- move players.[currentPlayer] roll;

            Console.WriteLine(players.[currentPlayer].Name
                                + "'s new location is "
                                + players.[currentPlayer].Place.ToString());
            this.askQuestion();
        gameState

    member private this.askQuestion() =
        let questionsStack = questionsStacks.[this.currentCategory()]
        Console.WriteLine("The category is " + questionsStack.Name)
        Console.WriteLine(questionsStack.Questions |> List.head)
        questionsStacks <- 
            questionsStacks
            |> List.map (fun x -> if x = questionsStack then { questionsStack with Questions = List.tail questionsStack.Questions } else x)

    member private this.currentCategory() =
        players.[currentPlayer].Place % 4
        
    member this.wasCorrectlyAnswered gameState =
        if players.[currentPlayer].IsInPenaltyBox then
            if isGettingOutOfPenaltyBox then
                Console.WriteLine("Answer was correct!!!!");
                players.[currentPlayer] <- winAGoldCoin players.[currentPlayer];
                Console.WriteLine(players.[currentPlayer].Name
                                    + " now has "
                                    + players.[currentPlayer].GoldCoins.ToString()
                                    + " Gold Coins.");
            else ()
        else
            Console.WriteLine("Answer was corrent!!!!");
            players.[currentPlayer] <- winAGoldCoin players.[currentPlayer];
            Console.WriteLine(players.[currentPlayer].Name
                                + " now has "
                                + players.[currentPlayer].GoldCoins.ToString()
                                + " Gold Coins.");

        let player = players.[currentPlayer]
        let winner = this.didPlayerWin();

        currentPlayer <- currentPlayer + 1;
        if (currentPlayer = players.Count) then currentPlayer <- 0;
        nextPlayer player <| if winner then gameState else Won;

    member this.wrongAnswer gameState =
        let playerInPenaltyBox = goToPenaltyBox players.[currentPlayer]
        players.[currentPlayer] <- playerInPenaltyBox;

        currentPlayer <- currentPlayer + 1;
        if (currentPlayer = players.Count) then currentPlayer <- 0;
        nextPlayer playerInPenaltyBox gameState;


    member private this.didPlayerWin(): bool =
        not (players.[currentPlayer].GoldCoins = 6);


module GameRunner = 
    [<EntryPoint>]
    let main argv = 
        let mutable isFirstRound = true;
        let mutable notAWinner = false;
        let aGame = Game();

        aGame.add("Chet") |> ignore;
        aGame.add("Pat") |> ignore;
        aGame.add("Sue") |> ignore;

        let rand = 
            match Array.toList argv with
            | seed::tail -> new Random(int seed)
            | _ -> new Random()

        let mutable gameState =
            WaitingPlayers
            |> addPlayer "Chet"
            |> addPlayer "Pat"
            |> addPlayer "Sue" 

        while isFirstRound || notAWinner do
            isFirstRound <- false; 
            gameState <- aGame.roll gameState (rand.Next(5) + 1);

            if (rand.Next(9) = 7) then
                gameState <- aGame.wrongAnswer gameState;
            else
                gameState <- aGame.wasCorrectlyAnswered gameState;

            notAWinner <- match gameState with Won -> false | _ -> true
        0
