<?php


namespace Trivia\Domain;


interface GameFacade
{
    function newGame() : string;

    function addPlayer(string $gameId, string $playerName);

    function roll(string $gameId);
}