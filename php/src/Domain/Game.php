<?php

namespace Trivia\Domain;

use Trivia\Domain\Event\DiceRolled;
use Trivia\Domain\Event\PlayerAdded;
use Trivia\Domain\Event\PlayerGetOutOfPenaltyBox;
use Trivia\Domain\Event\PlayerKeptInPenaltyBox;
use Trivia\Domain\Event\QuestionAsked;

class Game
{
    /**
     * @var Players
     */
    var $players;

    /**
     * @var QuestionsDecks
     */
    var $questionsDecks;

    var $isGettingOutOfPenaltyBox;

    /**
     * @var Closure
     */
    private $outputWriter;

    private function outputWriter($string)
    {
        $outputWriter = $this->outputWriter;
        $outputWriter($string);
    }

    function __construct(\Closure $outputWriter, QuestionsDecks $questionsDecks)
    {
        $this->players = new Players();
        $this->outputWriter = $outputWriter;
        $this->questionsDecks = $questionsDecks;
    }

    function add($playerName): PlayerAdded
    {
        $this->players->addPlayer($playerName);
        return new PlayerAdded($playerName, $this->players->getPlayersNumber());
    }

    function roll($roll)
    {
        $events = [];
        array_push($events, new DiceRolled($this->players->getCurrentPlayer()->getName(), $roll));

        if ($this->players->getCurrentPlayer()->isInPenaltyBox()) {
            if ($roll % 2 != 0) {
                $this->isGettingOutOfPenaltyBox = true;
                array_push($events, new PlayerGetOutOfPenaltyBox($this->players->getCurrentPlayer()->getName()));
                array_push($events, ...$this->moveAndAskQuestion($roll));
            } else {
                array_push($events, new PlayerKeptInPenaltyBox($this->players->getCurrentPlayer()->getName()));
                $this->isGettingOutOfPenaltyBox = false;
            }
        } else {
            array_push($events, ...$this->moveAndAskQuestion($roll));
        }
        return $events;
    }

    function wasCorrectlyAnswered(): bool
    {
        if ($this->players->getCurrentPlayer()->isInPenaltyBox()) {
            if ($this->isGettingOutOfPenaltyBox) {
                $this->outputWriter("Answer was correct!!!!");
                $this->winAGoldCoin();

                return $this->switchToNextPlayer();
            } else {
                return $this->switchToNextPlayer();
            }
        } else {
            $this->outputWriter("Answer was correct!!!!");
            $this->winAGoldCoin();

            return $this->switchToNextPlayer();
        }
    }

    function wrongAnswer(): bool
    {
        $this->outputWriter("Question was incorrectly answered");
        $this->outputWriter($this->players->getCurrentPlayer()->getName() . " was sent to the penalty box");
        $this->players->getCurrentPlayer()->goToPenaltyBox();

        return $this->switchToNextPlayer();
    }

    /**
     * @param $roll
     */
    private function moveAndAskQuestion($roll): array
    {
        $events = [];
        array_push($events, ...$this->players->getCurrentPlayer()->move($roll));

        $location = $this->players->getCurrentPlayer()->getLocation();
        $question = $this->questionsDecks->drawQuestion($location);
        array_push($events, new QuestionAsked($question));
        return $events;
    }

    private function switchToNextPlayer(): bool
    {
        return $this->players->switchToNextPlayer();
    }

    private function winAGoldCoin(): void
    {
        $this->players->getCurrentPlayer()->winAGoldCoin();
        $this->outputWriter($this->players->getCurrentPlayer()->getName()
            . " now has "
            . $this->players->getCurrentPlayer()->getGoldCoins()
            . " Gold Coins.");
    }
}
