<?php


namespace Trivia;


class Player
{
    private $name;
    private $location;

    /**
     * Player constructor.
     * @param $name
     */
    public function __construct($name)
    {
        $this->name = $name;
        $this->location = 0;
    }

    /**
     * @return string
     */
    public function getName()
    {
        return $this->name;
    }

    /**
     * @return int
     */
    public function getLocation()
    {
        return $this->location;
    }

    public function move($roll)
    {
        $this->location = $this->location + $roll;
        if ($this->location > 11) $this->location = $this->location - 12;
    }
}