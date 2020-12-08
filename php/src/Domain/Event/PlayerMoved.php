<?php


namespace Trivia\Domain\Event;


class PlayerMoved
{
    /**
     * @var string
     */
    public $playerName;
    /**
     * @var int
     */
    public $location;

    /**
     * PlayerMoved constructor.
     * @param $playerName
     * @param int $location
     */
    public function __construct(string $playerName, int $location)
    {
        $this->playerName = $playerName;
        $this->location = $location;
    }
}