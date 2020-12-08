<?php

namespace Trivia\Domain;

class Game
{
    /**
     * @var Players
     */
    var $players;

    /**
     * @var Questions
     */
    var $questions;

    var $isGettingOutOfPenaltyBox;
    /**
     * @var Closure
     */
    private $println;

    private function echoln($string)
    {
        $println = $this->println;
        $println($string);
    }

    function __construct(\Closure $println)
    {
        $this->players = new Players();
        $this->questions = new Questions();
        $this->println = $println;
    }

    function add($playerName): bool
    {
        $this->players->addPlayer($playerName);
        $this->echoln($playerName . " was added");
        $this->echoln("They are player number " . $this->players->getPlayersNumber());
        return true;
    }

    function roll($roll)
    {
        $this->echoln($this->players->getCurrentPlayer()->getName() . " is the current player");
        $this->echoln("They have rolled a " . $roll);

        if ($this->players->getCurrentPlayer()->isInPenaltyBox()) {
            if ($roll % 2 != 0) {
                $this->isGettingOutOfPenaltyBox = true;

                $this->echoln($this->players->getCurrentPlayer()->getName() . " is getting out of the penalty box");
                $this->moveAndAskQuestion($roll);
            } else {
                $this->echoln($this->players->getCurrentPlayer()->getName() . " is not getting out of the penalty box");
                $this->isGettingOutOfPenaltyBox = false;
            }
        } else {
            $this->moveAndAskQuestion($roll);
        }
    }

    function wasCorrectlyAnswered(): bool
    {
        if ($this->players->getCurrentPlayer()->isInPenaltyBox()) {
            if ($this->isGettingOutOfPenaltyBox) {
                $this->echoln("Answer was correct!!!!");
                $this->winAGoldCoin();

                return $this->switchToNextPlayer();
            } else {
                return $this->switchToNextPlayer();
            }
        } else {
            $this->echoln("Answer was corrent!!!!");
            $this->winAGoldCoin();

            return $this->switchToNextPlayer();
        }
    }

    function wrongAnswer(): bool
    {
        $this->echoln("Question was incorrectly answered");
        $this->echoln($this->players->getCurrentPlayer()->getName() . " was sent to the penalty box");
        $this->players->getCurrentPlayer()->goToPenaltyBox();

        return $this->switchToNextPlayer();
    }

    /**
     * @param $roll
     */
    private function moveAndAskQuestion($roll): void
    {
        $this->players->getCurrentPlayer()->move($roll);

        $this->echoln($this->players->getCurrentPlayer()->getName()
            . "'s new location is "
            . $this->players->getCurrentPlayer()->getLocation());

        $location = $this->players->getCurrentPlayer()->getLocation();
        $question = $this->questions->drawQuestion($location);
        $this->echoln("The category is " . $question->getCategory());
        $this->echoln($question->getText());
    }

    private function switchToNextPlayer(): bool
    {
        return $this->players->switchToNextPlayer();
    }

    private function winAGoldCoin(): void
    {
        $this->players->getCurrentPlayer()->winAGoldCoin();
        $this->echoln($this->players->getCurrentPlayer()->getName()
            . " now has "
            . $this->players->getCurrentPlayer()->getGoldCoins()
            . " Gold Coins.");
    }
}
