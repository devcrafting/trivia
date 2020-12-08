<?php

namespace Trivia;

use Trivia\Domain\Event\PlayerAdded;
use Trivia\Domain\Game;
use Trivia\Infra\GeneratedQuestions;

class GameRunner
{
    public static function run(\Closure $println)
    {
        $winner = false;

        $aGame = new Game($println, new GeneratedQuestions());

        GameRunner::displayPlayerAdded($println, $aGame->add("Chet"));
        GameRunner::displayPlayerAdded($println, $aGame->add("Pat"));
        GameRunner::displayPlayerAdded($println, $aGame->add("Sue"));

        do {
            $aGame->roll(rand(0, 5) + 1);

            if (rand(0, 9) == 7) {
                $winner = $aGame->wrongAnswer();
            } else {
                $winner = $aGame->wasCorrectlyAnswered();
            }
        } while (!$winner);
    }

    public static function displayPlayerAdded(\Closure $println, PlayerAdded $event) {
        $println($event->playerName . " was added");
        $println("They are player number " . $event->numberOfPlayers);
    }
}
