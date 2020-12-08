<?php

namespace Trivia;

use Trivia\Domain\Game;
use Trivia\Infra\GeneratedQuestions;

class GameRunner
{
    public static function run(\Closure $println)
    {
        $winner = false;

        $aGame = new Game($println, new GeneratedQuestions());

        $aGame->add("Chet");
        $aGame->add("Pat");
        $aGame->add("Sue");

        do {
            $aGame->roll(rand(0, 5) + 1);

            if (rand(0, 9) == 7) {
                $winner = $aGame->wrongAnswer();
            } else {
                $winner = $aGame->wasCorrectlyAnswered();
            }
        } while (!$winner);
    }
}
