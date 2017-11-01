#load "Game.fs"
#load "UglyGame.fs"

open UglyTrivia.GameRunner

[0..100]
|> List.iter (fun x -> main [|x.ToString()|] |> ignore)