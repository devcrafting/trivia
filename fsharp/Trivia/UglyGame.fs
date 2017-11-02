
namespace UglyTrivia

open System;
open System.Collections.Generic;
open System.Linq;
open System.Text;
open TriviaGame

type Game() as this =

    let players = List<Player>()

    let purses = Array.create 6 0

    let inPenaltyBox = Array.create 6 false

    let popQuestions = LinkedList<string>()
    let scienceQuestions = LinkedList<string>()
    let sportsQuestions = LinkedList<string>()
    let rockQuestions = LinkedList<string>()

    let mutable currentPlayer = 0;
    let mutable isGettingOutOfPenaltyBox = false;

    do
        for i = 1 to 50 do
            popQuestions.AddLast("Pop Question " + i.ToString()) |> ignore
            scienceQuestions.AddLast("Science Question " + i.ToString()) |> ignore
            sportsQuestions.AddLast("Sports Question " + i.ToString()) |> ignore
            rockQuestions.AddLast(this.createRockQuestion(i)) |> ignore
    
    member this.createRockQuestion(index: int): string =
        "Rock Question " + index.ToString()

    member this.isPlayable(): bool =
        this.howManyPlayers() >= 2

    member this.add(playerName: String): bool =
        players.Add(Player.WithName playerName);
        purses.[this.howManyPlayers()] <- 0;
        inPenaltyBox.[this.howManyPlayers()] <- false;

        Console.WriteLine(playerName + " was added");
        Console.WriteLine("They are player number " + players.Count.ToString());
        true

    member this.howManyPlayers(): int =
        players.Count;

    member this.roll(roll: int) =
        Console.WriteLine(players.[currentPlayer].Name + " is the current player");
        Console.WriteLine("They have rolled a " + roll.ToString());

        if inPenaltyBox.[currentPlayer] then
            if roll % 2 <> 0 then
                isGettingOutOfPenaltyBox <- true;

                Console.WriteLine(players.[currentPlayer].Name + " is getting out of the penalty box");
                players.[currentPlayer] <- players.[currentPlayer] |> move roll
                Console.WriteLine("The category is " + this.currentCategory());
                this.askQuestion();
               
            else
                Console.WriteLine(players.[currentPlayer].Name + " is not getting out of the penalty box");
                isGettingOutOfPenaltyBox <- false;

        else
            players.[currentPlayer] <- players.[currentPlayer] |> move roll
            Console.WriteLine("The category is " + this.currentCategory());
            this.askQuestion();

    member private this.askQuestion() =
        if this.currentCategory() = "Pop" then
            Console.WriteLine(popQuestions.First.Value);
            popQuestions.RemoveFirst();
            
        if this.currentCategory() = "Science" then
            Console.WriteLine(scienceQuestions.First.Value);
            scienceQuestions.RemoveFirst();
        
        if this.currentCategory() = "Sports" then
            Console.WriteLine(sportsQuestions.First.Value);
            sportsQuestions.RemoveFirst();

        if this.currentCategory() = "Rock" then
            Console.WriteLine(rockQuestions.First.Value);
            rockQuestions.RemoveFirst();


    member private this.currentCategory(): String =
        if (players.[currentPlayer].Location % 4 = 0) then "Pop"
        elif (players.[currentPlayer].Location % 4 = 1) then "Science"
        elif (players.[currentPlayer].Location % 4 = 2) then "Sports"
        else "Rock"

    member this.wasCorrectlyAnswered(): bool =
        if inPenaltyBox.[currentPlayer] then
            if isGettingOutOfPenaltyBox then
                Console.WriteLine("Answer was correct!!!!");
                purses.[currentPlayer] <- purses.[currentPlayer] + 1;
                Console.WriteLine(players.[currentPlayer].Name
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
            Console.WriteLine(players.[currentPlayer].Name
                                + " now has "
                                + purses.[currentPlayer].ToString()
                                + " Gold Coins.");

            let winner = this.didPlayerWin();
            currentPlayer <- currentPlayer + 1;
            if (currentPlayer = players.Count) then currentPlayer <- 0;

            winner;

    member this.wrongAnswer(): bool=
        Console.WriteLine("Question was incorrectly answered");
        Console.WriteLine(players.[currentPlayer].Name + " was sent to the penalty box");
        inPenaltyBox.[currentPlayer] <- true;

        currentPlayer <- currentPlayer + 1;
        if (currentPlayer = players.Count) then currentPlayer <- 0;
        true;


    member private this.didPlayerWin(): bool =
        not (purses.[currentPlayer] = 6);
