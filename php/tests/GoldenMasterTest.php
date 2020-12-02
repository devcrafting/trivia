<?php

use ApprovalTests\Approvals;
use PHPUnit\Framework\TestCase;
use Trivia\GameRunner;

class GoldenMasterTest extends TestCase
{
    public function testVerifyGameRunner()
    {
        srand(0);
        $result = '';
        $println = function($text) use (&$result) {
            $result = $result.$text."\n";
        };
        for ($i = 0; $i < 100; $i++) {
            GameRunner::run($println);
        }
        Approvals::verifyString($result);
    }
}