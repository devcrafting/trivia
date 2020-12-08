<?php


namespace Trivia\Infra;


use Trivia\Domain\EventPublisher;

class EventDispatcher implements EventPublisher
{
    private $handlerByType;

    public function publish($events)
    {
        foreach ($events as $event) {
            $handler = $this->handlerByType[get_class($event)];
            $handler($event);
        }
    }

    public function register(string $eventType, \Closure $handler)
    {
        $this->handlerByType[$eventType] = $handler;
    }
}