<?php

namespace Trivia;

class GameRunner
{
    public static function run(\Closure $println)
    {
        $notAWinner;

        $aGame = new Game($println);

        $aGame->add("Chet");
        $aGame->add("Pat");
        $aGame->add("Sue");


        do {

            $aGame->roll(rand(0, 5) + 1);

            if (rand(0, 9) == 7) {
                $notAWinner = $aGame->wrongAnswer();
            } else {
                $notAWinner = $aGame->wasCorrectlyAnswered();
            }


        } while ($notAWinner);
    }
}
