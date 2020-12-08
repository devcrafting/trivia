<?php


namespace Trivia\Domain\Event;


class PlayerSentToPenaltyBox
{
    public $playerName;

    /**
     * PlayerSentToPenaltyBox constructor.
     * @param $playerName
     */
    public function __construct($playerName)
    {
        $this->playerName = $playerName;
    }
}