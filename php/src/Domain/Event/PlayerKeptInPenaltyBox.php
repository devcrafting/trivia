<?php


namespace Trivia\Domain\Event;


class PlayerKeptInPenaltyBox
{
    /**
     * @var string
     */
    public $playerName;

    /**
     * PlayerKeptInPenaltyBox constructor.
     * @param string $playerName
     */
    public function __construct(string $playerName)
    {
        $this->playerName = $playerName;
    }
}