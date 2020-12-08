<?php


namespace Trivia\Domain;


use Trivia\Domain\Event\GoldCoinWon;
use Trivia\Domain\Event\PlayerMoved;
use Trivia\Domain\Event\PlayerWonGame;

class Player
{
    private $name;
    private $location = 0;
    private $goldCoins = 0;
    private $isInPenaltyBox = false;

    /**
     * Player constructor.
     * @param $name
     */
    public function __construct($name)
    {
        $this->name = $name;
    }

    public function getName(): string
    {
        return $this->name;
    }

    public function getLocation(): int
    {
        return $this->location;
    }

    public function move($roll) : array
    {
        $this->location = $this->location + $roll;
        if ($this->location > 11) $this->location = $this->location - 12;

        return array(new PlayerMoved($this->name, $this->location));
    }

    public function hasPlayerWon(): bool
    {
        return $this->goldCoins == 6;
    }

    public function winAGoldCoin() : array
    {
        $this->goldCoins++;
        $events = [];
        array_push($events, new GoldCoinWon($this->name, $this->goldCoins));
        if ($this->hasPlayerWon())
            array_push($events, new PlayerWonGame($this->name));
        return $events;
    }

    public function isInPenaltyBox() : bool
    {
        return $this->isInPenaltyBox;
    }

    public function goToPenaltyBox()
    {
        $this->isInPenaltyBox = true;
    }
}