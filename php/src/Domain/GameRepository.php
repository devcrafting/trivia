<?php

namespace Trivia\Domain\Domain;

interface GameRepository
{
    public function save(Game $game);

    public function get(string $gameId) : Game;
}