<?php

require_once "vendor/autoload.php";

use Trivia\GameRunner;

srand(0);

$echoln = function ($string) {
    echo $string."\n";
};

for ($i = 0; $i < 100; $i++) {
    GameRunner::run($echoln);
}
