<?php


namespace Trivia\Domain\Event;


use Trivia\Domain\Question;

class QuestionAsked
{
    /**
     * @var Question
     */
    public $question;

    /**
     * QuestionAsked constructor.
     * @param Question $question
     */
    public function __construct(Question $question)
    {
        $this->question = $question;
    }
}