<?php


namespace Trivia;


class Question
{
    /**
     * @var string
     */
    private $category;

    /**
     * @var string
     */
    private $text;

    public function __construct(string $category, string $text)
    {
        $this->category = $category;
        $this->text = $text;
    }

    public function getCategory(): string
    {
        return $this->category;
    }

    public function getText(): string
    {
        return $this->text;
    }
}