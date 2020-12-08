<?php

namespace Trivia\Domain;

use Trivia\Domain\Event\PlayerAdded;

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
        $this->outputWriter($this->players->getCurrentPlayer()->getName() . " is the current player");
        $this->outputWriter("They have rolled a " . $roll);

        if ($this->players->getCurrentPlayer()->isInPenaltyBox()) {
            if ($roll % 2 != 0) {
                $this->isGettingOutOfPenaltyBox = true;

                $this->outputWriter($this->players->getCurrentPlayer()->getName() . " is getting out of the penalty box");
                $this->moveAndAskQuestion($roll);
            } else {
                $this->outputWriter($this->players->getCurrentPlayer()->getName() . " is not getting out of the penalty box");
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
                $this->outputWriter("Answer was correct!!!!");
                $this->winAGoldCoin();

                return $this->switchToNextPlayer();
            } else {
                return $this->switchToNextPlayer();
            }
        } else {
            $this->outputWriter("Answer was corrent!!!!");
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
    private function moveAndAskQuestion($roll): void
    {
        $this->players->getCurrentPlayer()->move($roll);

        $this->outputWriter($this->players->getCurrentPlayer()->getName()
            . "'s new location is "
            . $this->players->getCurrentPlayer()->getLocation());

        $location = $this->players->getCurrentPlayer()->getLocation();
        $question = $this->questionsDecks->drawQuestion($location);
        $this->outputWriter("The category is " . $question->getCategory());
        $this->outputWriter($question->getText());
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
