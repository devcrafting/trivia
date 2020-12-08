<?php


namespace Trivia\Infra;


use Trivia\Domain\Event\DiceRolled;
use Trivia\Domain\Event\GoldCoinWon;
use Trivia\Domain\Event\PlayerAdded;
use Trivia\Domain\Event\PlayerGetOutOfPenaltyBox;
use Trivia\Domain\Event\PlayerKeptInPenaltyBox;
use Trivia\Domain\Event\PlayerMoved;
use Trivia\Domain\Event\PlayerSentToPenaltyBox;
use Trivia\Domain\Event\PlayerWonGame;
use Trivia\Domain\Event\QuestionAsked;
use Trivia\Domain\EventPublisher;

class ConsoleWriter
{
    /**
     * @var \Closure
     */
    private $println;

    /**
     * ConsoleHandlers constructor.
     * @param \Closure $println
     * @param EventPublisher $eventPublisher
     */
    public function __construct(\Closure $println, EventPublisher $eventPublisher)
    {
        $this->println = $println;
        $eventPublisher->register(PlayerAdded::class, function ($event) {
            $this->handlePlayerAdded($event);
        });
        $eventPublisher->register(DiceRolled::class, function ($event) {
            $this->handleDiceRolled($event);
        });
        $eventPublisher->register(PlayerGetOutOfPenaltyBox::class, function ($event) {
            $this->handlerPlayerGetOutOfPenaltyBox($event);
        });
        $eventPublisher->register(PlayerKeptInPenaltyBox::class, function ($event) {
            $this->handlerPlayerKeptInPenaltyBox($event);
        });
        $eventPublisher->register(PlayerMoved::class, function ($event) {
            $this->handlerPlayerMoved($event);
        });
        $eventPublisher->register(QuestionAsked::class, function ($event) {
            $this->handlerQuestionAsked($event);
        });
        $eventPublisher->register(GoldCoinWon::class, function ($event) {
            $this->handlerGoldCoinWon($event);
        });
        $eventPublisher->register(PlayerWonGame::class, function ($event) { });
        $eventPublisher->register(PlayerSentToPenaltyBox::class, function ($event) {
            $this->handlerPlayerSentToPenaltyBox($event);
        });
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

    private function handlerPlayerSentToPenaltyBox(PlayerSentToPenaltyBox $event)
    {
        ($this->println)("Question was incorrectly answered");
        ($this->println)($event->playerName . " was sent to the penalty box");
    }
}