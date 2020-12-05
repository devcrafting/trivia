<?php

namespace Trivia;

class Game
{
    /**
     * @var Player[]
     */
    var $players = array();

    var $popQuestions;
    var $scienceQuestions;
    var $sportsQuestions;
    var $rockQuestions;

    var $currentPlayer = 0;
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
        array_push($this->players, new Player($playerName));

        $this->echoln($playerName . " was added");
        $this->echoln("They are player number " . count($this->players));
        return true;
    }

    function howManyPlayers(): int
    {
        return count($this->players);
    }

    function roll($roll)
    {
        $this->echoln($this->players[$this->currentPlayer]->getName() . " is the current player");
        $this->echoln("They have rolled a " . $roll);

        if ($this->players[$this->currentPlayer]->isInPenaltyBox()) {
            if ($roll % 2 != 0) {
                $this->isGettingOutOfPenaltyBox = true;

                $this->echoln($this->players[$this->currentPlayer]->getName() . " is getting out of the penalty box");
                $this->moveAndAskQuestion($roll);
            } else {
                $this->echoln($this->players[$this->currentPlayer]->getName() . " is not getting out of the penalty box");
                $this->isGettingOutOfPenaltyBox = false;
            }
        } else {
            $this->moveAndAskQuestion($roll);
        }
    }

    function askQuestion()
    {
        if ($this->currentCategory() == "Pop")
            $this->echoln(array_shift($this->popQuestions));
        if ($this->currentCategory() == "Science")
            $this->echoln(array_shift($this->scienceQuestions));
        if ($this->currentCategory() == "Sports")
            $this->echoln(array_shift($this->sportsQuestions));
        if ($this->currentCategory() == "Rock")
            $this->echoln(array_shift($this->rockQuestions));
    }

    function currentCategory(): string
    {
        if ($this->players[$this->currentPlayer]->getLocation() % 4 == 0) return "Pop";
        if ($this->players[$this->currentPlayer]->getLocation() % 4 == 1) return "Science";
        if ($this->players[$this->currentPlayer]->getLocation() % 4 == 2) return "Sports";
        return "Rock";
    }

    function wasCorrectlyAnswered(): bool
    {
        if ($this->players[$this->currentPlayer]->isInPenaltyBox()) {
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
        $this->echoln($this->players[$this->currentPlayer]->getName() . " was sent to the penalty box");
        $this->players[$this->currentPlayer]->goToPenaltyBox();

        return $this->switchToNextPlayer();
    }

    /**
     * @param $roll
     */
    private function moveAndAskQuestion($roll): void
    {
        $this->players[$this->currentPlayer]->move($roll);

        $this->echoln($this->players[$this->currentPlayer]->getName()
            . "'s new location is "
            . $this->players[$this->currentPlayer]->getLocation());
        $this->echoln("The category is " . $this->currentCategory());
        $this->askQuestion();
    }

    /**
     * @return bool
     */
    private function switchToNextPlayer(): bool
    {
        $winner = $this->players[$this->currentPlayer]->hasPlayerWon();
        $this->currentPlayer++;
        if ($this->currentPlayer == count($this->players)) $this->currentPlayer = 0;

        return $winner;
    }

    private function winAGoldCoin(): void
    {
        $this->players[$this->currentPlayer]->winAGoldCoin();
        $this->echoln($this->players[$this->currentPlayer]->getName()
            . " now has "
            . $this->players[$this->currentPlayer]->getGoldCoins()
            . " Gold Coins.");
    }
}
