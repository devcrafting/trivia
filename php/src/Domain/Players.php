<?php

namespace Trivia\Domain;

class Players
{
    /**
     * @var Player[]
     */
    private $players = array();
    private $currentPlayer = 0;

    /**
     * Players constructor.
     */
    public function __construct()
    {
    }

    public function addPlayer($playerName)
    {
        array_push($this->players, new Player($playerName));
    }

    public function getCurrentPlayer(): Player
    {
        return $this->players[$this->currentPlayer];
    }

    /**
     * @return bool
     */
    public function switchToNextPlayer(): bool
    {
        $winner = $this->players[$this->currentPlayer]->hasPlayerWon();
        $this->currentPlayer++;
        if ($this->currentPlayer == count($this->players)) $this->currentPlayer = 0;

        return $winner;
    }

    public function getPlayersNumber(): int
    {
        return count($this->players);
    }

}