module TriviaGame

type Player = {
    Name: string
    Place: int
}

let move player roll =
    { player with Place = (player.Place + roll) % 12 }