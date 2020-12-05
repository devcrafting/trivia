<?php


namespace Trivia;


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

    public function move($roll)
    {
        $this->location = $this->location + $roll;
        if ($this->location > 11) $this->location = $this->location - 12;
    }

    public function hasPlayerWon(): bool
    {
        return $this->goldCoins == 6;
    }

    public function winAGoldCoin()
    {
        $this->goldCoins++;
    }

    public function getGoldCoins() : int
    {
        return $this->goldCoins;
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