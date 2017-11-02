#load "Game.fs"
#load "UglyGame.fs"
#load "GameRunner.fs"

open GameRunner

[0..100]
|> List.iter (fun x -> main [|x.ToString()|] |> ignore)