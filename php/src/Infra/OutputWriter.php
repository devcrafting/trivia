<?php


namespace Trivia\Infra;


use Trivia\Domain\Event\PlayerAdded;

class OutputWriter
{
    /**
     * @var \Closure
     */
    private $println;

    private $handlerByType;

    /**
     * ConsoleHandlers constructor.
     * @param \Closure $println
     */
    public function __construct(\Closure $println)
    {
        $this->println = $println;
        $this->handlerByType[PlayerAdded::class] = function ($event) {
            $this->handlePlayerAdded($event);
        };
    }

    private function handlePlayerAdded(PlayerAdded $event) {
        ($this->println)($event->playerName . " was added");
        ($this->println)("They are player number " . $event->numberOfPlayers);
    }

    public function publish($event)
    {
        $handler = $this->handlerByType[get_class($event)];
        $handler($event);
    }
}