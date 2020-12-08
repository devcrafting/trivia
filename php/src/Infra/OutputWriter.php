<?php


namespace Trivia\Infra;


use Trivia\Domain\Event\DiceRolled;
use Trivia\Domain\Event\GoldCoinWon;
use Trivia\Domain\Event\PlayerAdded;
use Trivia\Domain\Event\PlayerGetOutOfPenaltyBox;
use Trivia\Domain\Event\PlayerKeptInPenaltyBox;
use Trivia\Domain\Event\PlayerMoved;
use Trivia\Domain\Event\PlayerWonGame;
use Trivia\Domain\Event\QuestionAsked;

class OutputWriter
{
    /**
     * @var \Closure
     */
    private $println;

    private $handlerByType;

    /**
     * ConsoleHandlers constructor.
     * @param \Closure $println
     */
    public function __construct(\Closure $println)
    {
        $this->println = $println;
        $this->handlerByType[PlayerAdded::class] = function ($event) {
            $this->handlePlayerAdded($event);
        };
        $this->handlerByType[DiceRolled::class] = function ($event) {
            $this->handleDiceRolled($event);
        };
        $this->handlerByType[PlayerGetOutOfPenaltyBox::class] = function ($event) {
            $this->handlerPlayerGetOutOfPenaltyBox($event);
        };
        $this->handlerByType[PlayerKeptInPenaltyBox::class] = function ($event) {
            $this->handlerPlayerKeptInPenaltyBox($event);
        };
        $this->handlerByType[PlayerMoved::class] = function ($event) {
            $this->handlerPlayerMoved($event);
        };
        $this->handlerByType[QuestionAsked::class] = function ($event) {
            $this->handlerQuestionAsked($event);
        };
        $this->handlerByType[GoldCoinWon::class] = function ($event) {
            $this->handlerGoldCoinWon($event);
        };
        $this->handlerByType[PlayerWonGame::class] = function ($event) { };
    }

    private function handlePlayerAdded(PlayerAdded $event) {
        ($this->println)($event->playerName . " was added");
        ($this->println)("They are player number " . $event->numberOfPlayers);
    }

    private function handleDiceRolled(DiceRolled $event) {
        ($this->println)($event->playerName . " is the current player");
        ($this->println)("They have rolled a " . $event->roll);
    }

    private function handlerPlayerGetOutOfPenaltyBox(PlayerGetOutOfPenaltyBox $event) {
        ($this->println)($event->playerName . " is getting out of the penalty box");
    }

    public function publish($events)
    {
        foreach ($events as $event) {
            $handler = $this->handlerByType[get_class($event)];
            $handler($event);
        }
    }

    private function handlerPlayerKeptInPenaltyBox(PlayerKeptInPenaltyBox $event)
    {
        ($this->println)($event->playerName . " is not getting out of the penalty box");
    }

    private function handlerPlayerMoved(PlayerMoved $event)
    {
        ($this->println)($event->playerName
            . "'s new location is "
            . $event->location);
    }

    private function handlerQuestionAsked(QuestionAsked $event)
    {
        ($this->println)("The category is " . $event->question->getCategory());
        ($this->println)($event->question->getText());

    }

    private function handlerGoldCoinWon(GoldCoinWon $event)
    {
        ($this->println)("Answer was correct!!!!");
        ($this->println)($event->playerName
            . " now has "
            . $event->goldCoins
            . " Gold Coins.");
    }
}