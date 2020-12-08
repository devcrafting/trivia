<?php


namespace Trivia\Domain\Event;


class PlayerWonGame
{
    private $playerName;

    /**
     * PlayerWonGame constructor.
     * @param $playerName
     */
    public function __construct($playerName)
    {
        $this->playerName = $playerName;
    }
}