<?php

namespace Trivia\Domain;

interface EventPublisher
{
    public function register(string $eventType, \Closure $handler) : void;
    public function publish(array $events) : void;
}