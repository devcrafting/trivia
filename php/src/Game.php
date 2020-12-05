<?php

namespace Trivia;

class Game
{
    /**
     * @var Players
     */
    var $players;

    var $popQuestions;
    var $scienceQuestions;
    var $sportsQuestions;
    var $rockQuestions;

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
        $this->popQuestions = array();
        $this->scienceQuestions = array();
        $this->sportsQuestions = array();
        $this->rockQuestions = array();

        for ($i = 0; $i < 50; $i++) {
            array_push($this->popQuestions, "Pop Question " . $i);
            array_push($this->scienceQuestions, ("Science Question " . $i));
            array_push($this->sportsQuestions, ("Sports Question " . $i));
            array_push($this->rockQuestions, "Rock Question " . $i);
        }
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

    function askQuestion($location): Question
    {
        if ($location % 4 == 0)
            return new Question("Pop", array_shift($this->popQuestions));
        if ($location % 4 == 1)
            return new Question("Science", array_shift($this->scienceQuestions));
        if ($location % 4 == 2)
            return new Question("Sports", array_shift($this->sportsQuestions));
        return new Question("Rock", array_shift($this->rockQuestions));
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
        $question = $this->askQuestion($location);
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
