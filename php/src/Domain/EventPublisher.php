<?php

namespace Trivia\Domain;

interface EventPublisher
{
    public function register(string $eventType, \Closure $handler);
}