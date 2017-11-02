module TriviaGame

type Player = 
    {
        Name: string
        Location: int
    }
    static member WithName name =
        { Name = name; Location = 0 }

let move diceValue player =
    let newLocation = (player.Location + diceValue) % 12
    printfn "%s's new location is %i" player.Name newLocation
    { player with Location = newLocation }
