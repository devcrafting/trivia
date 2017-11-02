module GameRunner

open UglyTrivia
open System
open TriviaGame

[<EntryPoint>]
let main argv = 
    let mutable isFirstRound = true;
    let mutable notAWinner = false;
    let aGame = Game();

    aGame.add("Chet") |> ignore;
    aGame.add("Pat") |> ignore;
    aGame.add("Sue") |> ignore;

    let questionsStacks = [
        generateQuestionsStack "Pop"
        generateQuestionsStack "Science"
        generateQuestionsStack "Sports"
        generateQuestionsStack "Rock"
    ]

    let initialGameState =
        GameOutOfTheBox questionsStacks
        |> invitePlayer "Chet"
        |> invitePlayer "Pat"
        |> invitePlayer "Sue"

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
