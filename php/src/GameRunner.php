<?php

namespace Trivia;

use Trivia\Domain\Event\PlayerAdded;
use Trivia\Domain\Game;
use Trivia\Infra\ConsoleHandlers;
use Trivia\Infra\OutputWriter;
use Trivia\Infra\EventPublisher;
use Trivia\Infra\GeneratedQuestions;

class GameRunner
{
    public static function run(\Closure $println)
    {
        $winner = false;

        $aGame = new Game($println, new GeneratedQuestions());
        $consoleWriter = new OutputWriter($println);

        $consoleWriter->publish(array($aGame->add("Chet")));
        $consoleWriter->publish(array($aGame->add("Pat")));
        $consoleWriter->publish(array($aGame->add("Sue")));

        do {
            $consoleWriter->publish($aGame->roll(rand(0, 5) + 1));

            if (rand(0, 9) == 7) {
                $winner = $aGame->wrongAnswer();
            } else {
                $winner = $aGame->wasCorrectlyAnswered();
            }
        } while (!$winner);
    }
}
