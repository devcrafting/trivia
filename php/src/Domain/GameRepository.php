<?php

namespace Trivia\Domain;

interface GameRepository
{
    public function save(Game $game);

    public function get(string $gameId) : Game;
}