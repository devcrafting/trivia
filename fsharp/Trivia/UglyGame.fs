﻿
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

        Console.WriteLine(playerName + " was added");
        Console.WriteLine("They are player number " + players.Count.ToString());
        true

    member this.howManyPlayers(): int =
        players.Count;

    member this.roll(roll: int) =
        Console.WriteLine(players.[currentPlayer].Name + " is the current player");
        Console.WriteLine("They have rolled a " + roll.ToString());

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

    member private this.askQuestion() =
        let questionsStack = questionsStacks.[this.currentCategory()]
        Console.WriteLine("The category is " + questionsStack.Name)
        Console.WriteLine(questionsStack.Questions |> List.head)
        questionsStacks <- 
            questionsStacks
            |> List.map (fun x -> if x = questionsStack then { questionsStack with Questions = List.tail questionsStack.Questions } else x)

    member private this.currentCategory() =
        players.[currentPlayer].Place % 4
        
    member this.wasCorrectlyAnswered(): bool =
        if players.[currentPlayer].IsInPenaltyBox then
            if isGettingOutOfPenaltyBox then
                Console.WriteLine("Answer was correct!!!!");
                players.[currentPlayer] <- winAGoldCoin players.[currentPlayer];
                Console.WriteLine(players.[currentPlayer].Name
                                    + " now has "
                                    + players.[currentPlayer].GoldCoins.ToString()
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
            players.[currentPlayer] <- winAGoldCoin players.[currentPlayer];
            Console.WriteLine(players.[currentPlayer].Name
                                + " now has "
                                + players.[currentPlayer].GoldCoins.ToString()
                                + " Gold Coins.");

            let winner = this.didPlayerWin();
            currentPlayer <- currentPlayer + 1;
            if (currentPlayer = players.Count) then currentPlayer <- 0;

            winner;

    member this.wrongAnswer(): bool=
        Console.WriteLine("Question was incorrectly answered");
        Console.WriteLine(players.[currentPlayer].Name + " was sent to the penalty box");
        players.[currentPlayer] <- goToPenaltyBox players.[currentPlayer];

        currentPlayer <- currentPlayer + 1;
        if (currentPlayer = players.Count) then currentPlayer <- 0;
        true;


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

        while isFirstRound || notAWinner do
            isFirstRound <- false; 
            aGame.roll(rand.Next(5) + 1);

            if (rand.Next(9) = 7) then
                notAWinner <- aGame.wrongAnswer();
            else
                notAWinner <- aGame.wasCorrectlyAnswered();

        0
