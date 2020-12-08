<?php

namespace Trivia\Domain;

class Questions
{
    private $questions;

    /**
     * Questions constructor.
     */
    public function __construct()
    {
        $this->questions[0] = array();
        $this->questions[1] = array();
        $this->questions[2] = array();
        $this->questions[3] = array();

        for ($i = 0; $i < 50; $i++) {
            array_push($this->questions[0], new Question("Pop", "Pop Question " . $i));
            array_push($this->questions[1], new Question("Science", "Science Question " . $i));
            array_push($this->questions[2], new Question("Sports", "Sports Question " . $i));
            array_push($this->questions[3], new Question("Rock", "Rock Question " . $i));
        }
    }

    public function drawQuestion($location): Question
    {
        return array_shift($this->questions[$location % 4]);
    }
}