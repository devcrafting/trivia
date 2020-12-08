<?php

namespace Trivia;

use Trivia\Domain\Event\PlayerAdded;
use Trivia\Domain\Event\PlayerWonGame;
use Trivia\Domain\Game;
use Trivia\Infra\ConsoleHandlers;
use Trivia\Infra\ConsoleWriter;
use Trivia\Infra\EventDispatcher;
use Trivia\Infra\EventPublisher;
use Trivia\Infra\GeneratedQuestions;

class GameRunner
{
    public static function run(\Closure $println)
    {
        $winner = false;

        $aGame = new Game($println, new GeneratedQuestions());
        $eventPublisher = new EventDispatcher();
        new ConsoleWriter($println, $eventPublisher);

        $eventPublisher->publish(array($aGame->add("Chet")));
        $eventPublisher->publish(array($aGame->add("Pat")));
        $eventPublisher->publish(array($aGame->add("Sue")));

        do {
            $eventPublisher->publish($aGame->roll(rand(0, 5) + 1));

            if (rand(0, 9) == 7) {
                $events = $aGame->wrongAnswer();
                $eventPublisher->publish($events);
            } else {
                $events = $aGame->wasCorrectlyAnswered();
                $eventPublisher->publish($events);
            }
            $winner = count($events) > 1 && $events[1] instanceof PlayerWonGame;
        } while (!$winner);
    }
}
