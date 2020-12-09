<?php

use PHPUnit\Framework\Assert;
use PHPUnit\Framework\TestCase;
use Trivia\Domain\Event\PlayerMoved;
use Trivia\Infra\EventDispatcher;
use Trivia\SomethingHappened;

class EventDispatcherTest extends TestCase
{
    public function testCanCallSeveralHandlers() {
        $eventDispatcher = new EventDispatcher();
        $handler1Called = false;
        $handler2Called = true;
        $eventDispatcher->register(PlayerMoved::class, function($e) use (&$handler1Called) {
            $handler1Called = true;
        });
        $eventDispatcher->register(PlayerMoved::class, function($e) use (&$handler2Called) {
            $handler2Called = true;
        });

        $eventDispatcher->publish(array(new PlayerMoved("toto", 1)));

        Assert::isTrue()->evaluate($handler1Called);
        Assert::isTrue()->evaluate($handler2Called);
    }
}