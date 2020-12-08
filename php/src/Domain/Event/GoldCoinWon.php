<?php


namespace Trivia\Domain\Event;


class GoldCoinWon
{
    public $playerName;
    /**
     * @var int
     */
    public $goldCoins;

    /**
     * GoldCoinWon constructor.
     * @param $playerName
     * @param int $goldCoins
     */
    public function __construct($playerName, int $goldCoins)
    {
        $this->playerName = $playerName;
        $this->goldCoins = $goldCoins;
    }
}