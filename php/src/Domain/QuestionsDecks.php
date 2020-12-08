<?php

namespace Trivia\Domain;

interface QuestionsDecks
{
    public function drawQuestion($location): Question;
}