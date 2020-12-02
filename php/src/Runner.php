<?php

require_once "vendor/autoload.php";

use Trivia\GameRunner;

$echoln = function ($string) {
    echo $string."\n";
};

GameRunner::run($echoln);
