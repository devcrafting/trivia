<?php


namespace Trivia\Infra;


use Trivia\Domain\EventPublisher;

class EventDispatcher implements EventPublisher
{
    private $handlersByType = array();

    public function publish(array $events) : void
    {
        foreach ($events as $event) {
            $handlers = $this->handlersByType[get_class($event)];
            foreach ($handlers as $handler) {
                $handler($event);
            }
        }
    }

    public function register(string $eventType, \Closure $handler) : void
    {
        $hasHandlers = array_key_exists($eventType, $this->handlersByType);
        if ($hasHandlers) {
            $handlers = $this->handlersByType[$eventType];
        } else {
            $handlers = array();
        }
        array_push($handlers, $handler);
        $this->handlersByType[$eventType] = $handlers;
    }
}