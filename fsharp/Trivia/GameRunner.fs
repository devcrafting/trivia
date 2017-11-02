module GameRunner

open UglyTrivia
open System
open TriviaGame

[<EntryPoint>]
let main argv = 
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

    let randomizeAnswers turn =
        if (rand.Next(9) = 7) then
            answerBadly turn
        else
            answerCorrectly turn

    let rec nextTurn gameState =
        match gameState with
        | Playing turn ->
            turn
            |> roll (rand.Next(5) + 1)
            |> randomizeAnswers
            |> nextTurn
        | Won -> ()

    nextTurn initialGameState

    0
