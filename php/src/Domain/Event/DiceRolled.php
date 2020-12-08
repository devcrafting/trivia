<?php


namespace Trivia\Domain\Event;


class DiceRolled
{
    /**
     * @var string
     */
    public $playerName;
    /**
     * @var int
     */
    public $roll;

    /**
     * DiceRolled constructor.
     * @param string $playerName
     * @param $roll
     */
    public function __construct(string $playerName, int $roll)
    {
        $this->playerName = $playerName;
        $this->roll = $roll;
    }
}