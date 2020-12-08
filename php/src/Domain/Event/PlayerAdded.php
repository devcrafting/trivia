<?php

namespace Trivia\Domain\Event;

class PlayerAdded
{
    /**
     * @var string
     */
    public $playerName;

    /**
     * @var int
     */
    public $numberOfPlayers;

    public function __construct(string $playerName, int $numberOfPlayers)
    {
        $this->playerName = $playerName;
        $this->numberOfPlayers = $numberOfPlayers;
    }
}