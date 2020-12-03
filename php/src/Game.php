<?php

namespace Trivia;

class Game
{
    var $players;
    var $places;
    var $goldCoins;
    var $inPenaltyBox;

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
        $this->players = array();
        $this->places = array(0);
        $this->goldCoins = array(0);
        $this->inPenaltyBox = array(0);

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

    function add($playerName)
    {
        array_push($this->players, $playerName);
        $this->places[$this->howManyPlayers()] = 0;
        $this->goldCoins[$this->howManyPlayers()] = 0;
        $this->inPenaltyBox[$this->howManyPlayers()] = false;

        $this->echoln($playerName . " was added");
        $this->echoln("They are player number " . count($this->players));
        return true;
    }

    function howManyPlayers()
    {
        return count($this->players);
    }

    function roll($roll)
    {
        $this->echoln($this->players[$this->currentPlayer] . " is the current player");
        $this->echoln("They have rolled a " . $roll);

        if ($this->inPenaltyBox[$this->currentPlayer]) {
            if ($roll % 2 != 0) {
                $this->isGettingOutOfPenaltyBox = true;

                $this->echoln($this->players[$this->currentPlayer] . " is getting out of the penalty box");
                $this->moveAndAskQuestion($roll);
            } else {
                $this->echoln($this->players[$this->currentPlayer] . " is not getting out of the penalty box");
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

    function currentCategory()
    {
        if ($this->places[$this->currentPlayer] % 4 == 0) return "Pop";
        if ($this->places[$this->currentPlayer] % 4 == 1) return "Science";
        if ($this->places[$this->currentPlayer] % 4 == 2) return "Sports";
        return "Rock";
    }

    function wasCorrectlyAnswered()
    {
        if ($this->inPenaltyBox[$this->currentPlayer]) {
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

    function wrongAnswer()
    {
        $this->echoln("Question was incorrectly answered");
        $this->echoln($this->players[$this->currentPlayer] . " was sent to the penalty box");
        $this->inPenaltyBox[$this->currentPlayer] = true;

        return $this->switchToNextPlayer();
    }

    function didPlayerWin()
    {
        return !($this->goldCoins[$this->currentPlayer] == 6);
    }

    /**
     * @param $roll
     */
    private function moveAndAskQuestion($roll): void
    {
        $this->places[$this->currentPlayer] = $this->places[$this->currentPlayer] + $roll;
        if ($this->places[$this->currentPlayer] > 11) $this->places[$this->currentPlayer] = $this->places[$this->currentPlayer] - 12;

        $this->echoln($this->players[$this->currentPlayer]
            . "'s new location is "
            . $this->places[$this->currentPlayer]);
        $this->echoln("The category is " . $this->currentCategory());
        $this->askQuestion();
    }

    /**
     * @return bool
     */
    private function switchToNextPlayer(): bool
    {
        $winner = $this->didPlayerWin();
        $this->currentPlayer++;
        if ($this->currentPlayer == count($this->players)) $this->currentPlayer = 0;

        return $winner;
    }

    private function winAGoldCoin(): void
    {
        $this->goldCoins[$this->currentPlayer]++;
        $this->echoln($this->players[$this->currentPlayer]
            . " now has "
            . $this->goldCoins[$this->currentPlayer]
            . " Gold Coins.");
    }
}
