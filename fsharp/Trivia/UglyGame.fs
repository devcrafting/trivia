
namespace UglyTrivia

open System;
open System.Collections.Generic;
open System.Linq;
open System.Text;
open TriviaGame

type Game() =

    let players = List<Player>()
    let mutable questionsStacks = [
        generateQuestionsStack "Pop"
        generateQuestionsStack "Science"
        generateQuestionsStack "Sports"
        generateQuestionsStack "Rock"
    ]

    let mutable currentPlayer = 0;
    let mutable isGettingOutOfPenaltyBox = false;

    member this.isPlayable(): bool =
        this.howManyPlayers() >= 2

    member this.add(playerName: String): bool =
        players.Add(Player.WithName playerName)
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
                players.[currentPlayer] <- players.[currentPlayer] |> move roll
                this.askQuestion();
               
            else
                Console.WriteLine(players.[currentPlayer].Name + " is not getting out of the penalty box");
                isGettingOutOfPenaltyBox <- false;

        else
            players.[currentPlayer] <- players.[currentPlayer] |> move roll
            this.askQuestion();

    member private this.askQuestion() =
        let location = players.[currentPlayer].Location
        questionsStacks <- questionsStacks |> askAndDiscardQuestion location

    member this.wasCorrectlyAnswered(): bool =
        if players.[currentPlayer].IsInPenaltyBox then
            if isGettingOutOfPenaltyBox then
                Console.WriteLine("Answer was correct!!!!");
                players.[currentPlayer] <- players.[currentPlayer] |> winAGoldCoin
                
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
            players.[currentPlayer] <- players.[currentPlayer] |> winAGoldCoin

            let winner = this.didPlayerWin();
            currentPlayer <- currentPlayer + 1;
            if (currentPlayer = players.Count) then currentPlayer <- 0;

            winner;

    member this.wrongAnswer(): bool=
        players.[currentPlayer] <- players.[currentPlayer] |> goToPenaltyBox

        currentPlayer <- currentPlayer + 1;
        if (currentPlayer = players.Count) then currentPlayer <- 0;
        true;


    member private this.didPlayerWin(): bool =
        not (players.[currentPlayer].GoldCoins = 6);
