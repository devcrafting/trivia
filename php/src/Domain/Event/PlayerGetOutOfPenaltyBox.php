<?php


namespace Trivia\Domain\Event;


class PlayerGetOutOfPenaltyBox
{
    /**
     * @var string
     */
    public $playerName;

    /**
     * PlayerGetOutOfPenaltyBox constructor.
     * @param string $playerName
     */
    public function __construct(string $playerName)
    {
        $this->playerName = $playerName;
    }
}