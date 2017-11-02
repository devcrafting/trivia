module TriviaGame

type Player = 
    {
        Name: string
        Location: int
        GoldCoins: int
    }
    static member WithName name =
        { Name = name; Location = 0; GoldCoins = 0 }

let move diceValue player =
    let newLocation = (player.Location + diceValue) % 12
    printfn "%s's new location is %i" player.Name newLocation
    { player with Location = newLocation }

let winAGoldCoin player =
    let goldCoinsWon = player.GoldCoins + 1;
    printfn "%s now has %i Gold Coins." player.Name goldCoinsWon
    { player with GoldCoins = goldCoinsWon }
